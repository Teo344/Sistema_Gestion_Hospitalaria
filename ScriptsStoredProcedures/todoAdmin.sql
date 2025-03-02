USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspInsertarAdministrador]
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Administradores (Nombre, Apellido, Clave, Email)
    VALUES (@Nombre, @Apellido, @Clave, @Email);
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspActualizarAdministrador]
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Administradores
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Clave = @Clave,
        Email = @Email
    WHERE Id = @Id;
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspEliminarAdministrador]
    @Id INT
AS
BEGIN
    DELETE FROM Administradores WHERE Id = @Id;
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspObtenerAdministradores]
AS
BEGIN
    SELECT Id, Nombre, Apellido, Clave, Email
    FROM Administradores;
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspFiltrarAdministrador]
    @nombre NVARCHAR(100) = '',
    @apellido NVARCHAR(100) = '',
    @email NVARCHAR(100) = ''
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)

    SET @sql = 'SELECT Id, Nombre, Apellido, Clave, Email 
                FROM Administradores 
                WHERE 1=1'

    IF @nombre <> ''
        SET @sql = @sql + ' AND Nombre LIKE ''%' + @nombre + '%'''
    IF @apellido <> ''
        SET @sql = @sql + ' AND Apellido LIKE ''%' + @apellido + '%'''
    IF @email <> ''
        SET @sql = @sql + ' AND Email LIKE ''%' + @email + '%'''

    EXECUTE SP_EXECUTESQL @sql
END;
GO