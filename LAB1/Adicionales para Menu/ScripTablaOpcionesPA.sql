USE [DBCatalogo]
GO
/****** Object:  Table [dbo].[Opcion]    Script Date: 27/06/2023 17:50:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Opcion](
	[IdOpcion] [int] IDENTITY(1,1) NOT NULL,
	[NombreOpcion] [varchar](50) NULL,
	[UrlOpcion] [varchar](50) NULL,
	[RutaImagen] [varchar](50) NULL,
	[NroOrden] [int] NULL,
	[IdOpcionRef] [int] NULL,
 CONSTRAINT [PK_Opcion] PRIMARY KEY CLUSTERED 
(
	[IdOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Opcion] ON 

INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (1, N'Mantenimiento', N'#', N'fa fa-envelope-o', 1, 0)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (2, N'Productos', N'Producto/Index', N'fa fa-envelope-o', 1, 1)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (3, N'Vendedor', N'Vendedor/Index', N'fa fa-envelope-o', 2, 1)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (4, N'Maestras', N'#', N'fa fa-envelope-o', 3, 1)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (5, N'Categorias', N'Categoria/Index', N'fa fa-envelope-o', 1, 4)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (6, N'Roles', N'Rol/Index', N'fa fa-envelope-o', 2, 4)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (7, N'Ubigeo', N'Ubigeo/Index', N'fa fa-envelope-o', 3, 4)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (8, N'Seguridad', N'#', N'fa fa-database', 2, 0)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (9, N'Usuario', N'Usuarios/Index', N'fa fa-envelope-o', 1, 8)
INSERT [dbo].[Opcion] ([IdOpcion], [NombreOpcion], [UrlOpcion], [RutaImagen], [NroOrden], [IdOpcionRef]) VALUES (10, N'Ver Productos', N'Producto/Index', N'fa fa-fighter-jet', 3, 0)
SET IDENTITY_INSERT [dbo].[Opcion] OFF
/****** Object:  StoredProcedure [dbo].[paOpcionLista]    Script Date: 27/06/2023 17:50:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[paOpcionLista] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [IdOpcion],[NombreOpcion] , [UrlOpcion], [RutaImagen],[NroOrden],[IdOpcionRef] from Opcion
END




GO
