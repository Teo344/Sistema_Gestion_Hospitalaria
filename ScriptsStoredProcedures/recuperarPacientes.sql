USE [HospitalDB]  
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.uspRecuperarPaciente
    @idPaciente INT
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion
    FROM Pacientes
    WHERE Id = @idPaciente;
END
GO
