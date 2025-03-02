USE [HospitalDB]
GO
/****** Object:  StoredProcedure [dbo].[uspRecuperarPacientePorIdentificacion]    Script Date: 1/3/2025 14:28:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspRecuperarPacientePorIdentificacion]
    @Identificacion NVARCHAR(15)
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion, Identificacion
    FROM Pacientes
    WHERE Identificacion = @Identificacion;
END;