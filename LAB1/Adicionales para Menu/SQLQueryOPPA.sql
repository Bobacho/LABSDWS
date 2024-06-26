USE [DBCatalogo]
GO
/****** Object:  StoredProcedure [dbo].[paUsuario_BuscaCodUserClave]    Script Date: 28/06/2023 21:53:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[paUsuario_BuscaCodUserClave] 
	-- Add the parameters for the stored procedure here
	@Clave binary,
	@CodUsuario varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdUsuario,CodUsuario,Nombres From Usuario
	Where CodUsuario = @CodUsuario and Clave = @Clave
END


