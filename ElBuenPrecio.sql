USE MASTER
CREATE DATABASE ElBuenPrecio
USE ElBuenPrecio

CREATE TABLE [dbo].[Cajero](
	[Identificacion] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[PrimerApellido] [varchar](50) NOT NULL,
	[SegundoApellido] [varchar](50) NOT NULL,
	[CajaAsignada] [int] NOT NULL,
	[Activo][tinyint] NOT NULL,
 CONSTRAINT [PK_Cajero] PRIMARY KEY CLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Codigo] [int] NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Codigo] [int] NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[CodigoCategoria] [int] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleDeLaVenta](
	[CodigoVenta] [int] NOT NULL,
	[CodigoProducto] [int] NOT NULL,
	[PrecioUnitario] [decimal](18,2) NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[Codigo] [int] NOT NULL,
	[CodigoCajero] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Importe] [decimal] NOT NULL,
	CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([CodigoCategoria])
REFERENCES [dbo].[Categoria] ([Codigo])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO

ALTER TABLE [dbo].[DetalleDeLaVenta]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Producto] FOREIGN KEY([CodigoProducto])
REFERENCES [dbo].[Producto] ([Codigo])
GO
ALTER TABLE [dbo].[DetalleDeLaVenta] CHECK CONSTRAINT [FK_Detalle_Producto]
GO

ALTER TABLE [dbo].[DetalleDeLaVenta]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Venta] FOREIGN KEY([CodigoVenta])
REFERENCES [dbo].[Venta] ([Codigo])
GO
ALTER TABLE [dbo].[DetalleDeLaVenta] CHECK CONSTRAINT [FK_Detalle_Venta]
GO

ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Cajero] FOREIGN KEY([CodigoCajero])
REFERENCES [dbo].[Cajero] ([Identificacion])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Cajero]
GO
USE [master]
GO
ALTER DATABASE [BuenPrecio] SET  READ_WRITE 
GO