USE [HospitalDB]
GO
/****** Object:  StoredProcedure [dbo].[uspFiltrarPaciente]    Script Date: 23/2/2025 15:11:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[uspFiltrarPaciente]
    @nombre VARCHAR(100) = '',
    @apellido VARCHAR(100) = '',
    @email VARCHAR(100) = '',
    @telefono VARCHAR(20) = '',
    @fechaNacimiento DATE = NULL,
    @direccion NVARCHAR(255) = ''
AS
BEGIN
    DECLARE @sql NVARCHAR(500)

    SET @sql = 'SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion 
                FROM Pacientes 
                WHERE 1=1'

    IF @nombre <> ''
        SET @sql = @sql + ' AND Nombre LIKE ''%' + @nombre + '%'''
    IF @apellido <> ''
        SET @sql = @sql + ' AND Apellido LIKE ''%' + @apellido + '%'''
    IF @email <> ''
        SET @sql = @sql + ' AND Email LIKE ''%' + @email + '%'''
    IF @telefono <> ''
        SET @sql = @sql + ' AND Telefono LIKE ''%' + @telefono + '%'''
    IF @fechaNacimiento IS NOT NULL
        SET @sql = @sql + ' AND FechaNacimiento = ''' + CONVERT(NVARCHAR, @fechaNacimiento, 23) + ''''
    IF @direccion <> ''
        SET @sql = @sql + ' AND Direccion LIKE ''%' + @direccion + '%'''

    EXECUTE SP_EXECUTESQL @sql
END
