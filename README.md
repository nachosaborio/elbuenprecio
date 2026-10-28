# El Buen Precio

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4?logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=white)
![WinForms](https://img.shields.io/badge/UI-Windows%20Forms-0078D6?logo=windows&logoColor=white)
![Architecture](https://img.shields.io/badge/architecture-Client%E2%80%93Server%20%2B%20Multithreading-2b6cb0)

> ⚠️ **SQLite migration verified at build time (CI); runtime verification of the server–client flow is still pending.**
>
> Pending checks (run on a machine with Visual Studio):
> - [ ] Cashier login (active cashier `1` logs in; inactive cashier `2` is rejected)
> - [ ] List products (all products and search by name)
> - [ ] Checkout / invoice (sale stored, stock decreased)
> - [ ] Type round-trip: `Activo` (tinyint → INTEGER), prices (decimal → REAL/NUMERIC), sale date with `date('now','localtime')`
> - [ ] Native `e_sqlite3.dll` loads from `bin\Release\runtimes\win-x64\native`

A multi-cashier **client–server point-of-sale (POS) system** for a supermarket, built in **C#** with a custom **TCP + JSON messaging protocol**, **multithreading** (one thread per connected client), and an embedded **SQLite** database (via **Microsoft.Data.Sqlite**).

This was my final project for the Advanced Programming course of the Informatics Engineering degree at **Universidad Estatal a Distancia de Costa Rica (UNED)** — the project where I learned C#, multithreading, and SQL from the ground up.

> ⚠️ Academic project (2021). Written with .NET Framework 4.7.2 and Windows Forms. Preserved here as a learning milestone — it demonstrates hand-built concurrency and a hand-rolled network protocol, without relying on any higher-level framework. The original version used SQL Server; the data layer has since been migrated to SQLite so the project builds and runs without installing a database server.

---

## What it does

A central **server** manages the store's catalog and sales, while multiple **cashier clients** connect over the network at the same time. Each cashier can log in, browse and search the product catalog, check stock availability in real time, and register (invoice) a sale. The server keeps a live log of every client action and a running count of connected cashiers.

Because every client is handled on its own thread, several cashiers can operate concurrently against the same SQLite database — which is exactly what made this project a hands-on lesson in multithreading and shared-state access.

---

## Architecture

The system is split into four projects across three layers:

```
┌──────────────────────┐        TCP socket (127.0.0.1:16830)        ┌──────────────────────┐
│  ElBuenPrecioCliente │  ───  JSON "Mensajero<T>" messages,  ───▶  │ ElBuenPrecioServidor │
│  (WinForms cashier)  │        one line per request/response       │  (WinForms server)   │
└──────────────────────┘                                            └───────────┬──────────┘
                                                                                 │
                         ┌───────────────────────────────────────┐              │ ADO.NET
                         │        ElBuenPrecio.Entidades           │◀────────────┤
                         │  Shared models + Mensajero<T> envelope  │              ▼
                         └───────────────────────────────────────┘   ┌──────────────────────┐
                                                                      │   ElBuenPrecio.ADO    │
                                                                      │  (ADO.NET data layer) │
                                                                      └───────────┬──────────┘
                                                                                  ▼
                                                                  SQLite database (ElBuenPrecio.db)
```

| Project | Responsibility |
|---|---|
| **ElBuenPrecioServidor** | Windows Forms server. Opens a `TcpListener`, accepts clients, and spawns **one thread per client**. Dispatches incoming commands and updates a live activity log / connected-client counter. |
| **ElBuenPrecioCliente** | Windows Forms cashier app. Connects to the server, sends commands and renders catalog, stock checks, and invoicing. It never talks to the database directly. |
| **ElBuenPrecio.ADO** | Data-access layer. Raw **ADO.NET** with **Microsoft.Data.Sqlite** (`SqliteConnection` / `SqliteCommand` / `SqliteDataReader`). The connection string (`Data Source=ElBuenPrecio.db`) is read from the server's `App.config` and resolved next to the executable — no credentials are needed. |
| **ElBuenPrecio.Entidades** | Shared domain models (`Cajero`, `Producto`, `Categoria`, `Venta`, `DetalleDeVenta`, `TodaLaVenta`, `VerificadorDeStock`) plus the generic `Mensajero<T>` message envelope used by the protocol. |

### The messaging protocol

Client and server talk over a raw TCP stream using a small hand-built protocol. Every message is a JSON-serialized generic envelope, one message per line:

```csharp
Mensajero<T> {
    string Comando;   // the operation name, e.g. "Facturar"
    string Cliente;   // who is sending it
    T      Objeto;    // the typed payload
}
```

The server reads a line, deserializes it into a `Mensajero<T>`, and routes on `Comando`:

| Command | Action |
|---|---|
| `Conectar` | Log a cashier in (by ID) and report their activation state |
| `AgregarCajero` | Register a new cashier |
| `Desconectar` | Log a cashier out and update the connected count |
| `TodosLosProductos` | Return the full product catalog |
| `AlgunosProductos` | Search products by name |
| `HaySuficientes` | Check whether there is enough stock for a requested quantity |
| `Facturar` | Register a sale (invoice) and its line items |

Cross-thread UI updates (activity log, connected-client list, counter) are marshalled back onto the UI thread with `Control.Invoke` and delegates — so the per-client worker threads never touch WinForms controls directly.

---

## Database

The server creates the SQLite database automatically: on first start it creates `ElBuenPrecio.db` **next to the server executable**, builds the schema, and inserts seed data (cashier `1` active, cashier `2` not yet activated). There is nothing to install or run by hand.

The schema has five tables with referential integrity (`PRAGMA foreign_keys = ON`):

- **Cajero** — cashiers (id, name, assigned register, active flag)
- **Categoria** — product categories
- **Producto** — products (price, quantity on hand, FK → Categoria)
- **Venta** — sales headers (FK → Cajero, date, total)
- **DetalleDeLaVenta** — sale line items (FK → Venta, FK → Producto, unit price, quantity)

See [`docs/diagrama-bd.png`](docs/diagrama-bd.png) for the entity-relationship diagram.

The original SQL Server script is kept at [`db/legacy/ElBuenPrecio.sql`](db/legacy/ElBuenPrecio.sql) **for historical reference only** — it is not used to build or run the project.

---

## Tech stack

- **Language:** C# (.NET Framework 4.7.2)
- **UI:** Windows Forms
- **Networking:** `System.Net.Sockets` (`TcpListener` / `TcpClient`), raw TCP streams
- **Concurrency:** `System.Threading.Thread`, one worker thread per client
- **Serialization:** Newtonsoft.Json
- **Data access:** ADO.NET against **SQLite** (Microsoft.Data.Sqlite)

---

## Getting started

### Prerequisites

- Windows with **.NET Framework 4.7.2**
- **Visual Studio 2022** or **Build Tools for Visual Studio 2022** (workload ".NET desktop build tools")
- **`nuget.exe`** on your `PATH`

SQL Server is **not** required.

### 1. Clone

```powershell
git clone https://github.com/nachosaborio/elbuenprecio.git
cd elbuenprecio
```

### 2. Restore NuGet packages

```powershell
nuget restore ElBuenPrecio.sln
```

### 3. Build

`ElBuenPrecio.sln` is the single entry point for building all four projects:

```powershell
msbuild ElBuenPrecio.sln /p:Configuration=Release
```

### 4. Run

1. Start the server, `ElBuenPrecioServidor\ElBuenPrecioServidor\bin\Release\ElBuenPrecioServidor.exe`, and click **Encender** — it begins listening on `127.0.0.1:16830`. The database is created on this first start.
2. Start one or more cashier clients, `ElBuenPrecioCliente\ElBuenPrecioCliente\bin\Release\ElBuenPrecioCliente.exe`, and log in as cashier `1`.

[`docs/manual.docx`](docs/manual.docx) contains the original step-by-step usage guide with screenshots.

---

## Screenshots

| Update products | Update clients |
|---|---|
| ![Update products](docs/actualizar-productos.png) | ![Update clients](docs/actualizar-clientes.png) |

---

## What I learned

This was the project where C#, multithreading, and SQL "clicked" for me:

- Designing a **stateful client–server protocol** from scratch over raw TCP, instead of using HTTP/REST or a framework.
- Handling **concurrent clients safely** with a thread-per-connection model, and marshalling results back to the UI thread.
- A clean **layered architecture** (shared entities · data access · server · client) with a reusable generic message envelope.
- Modeling a relational schema with proper keys and foreign keys, and talking to it with raw ADO.NET.

---

## Maintainer notes

- **GitHub topic:** after the SQLite migration, update the repository topics by hand on GitHub (**About → ⚙ → Topics**): replace `sql-server` with `sqlite`. This can't be done from the code.

---

## Author

**Sergio Ignacio Saborío Segura**
Informatics Engineer — Universidad Estatal a Distancia de Costa Rica (UNED)
Advanced Programming final project · 2021
