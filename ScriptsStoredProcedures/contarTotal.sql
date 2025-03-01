USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE uspObtenerTotalPacientes
AS
BEGIN
    SELECT COUNT(*) AS TotalPacientes FROM Pacientes;
END;


CREATE PROCEDURE uspObtenerTotalEspecialidades
AS
BEGIN
    SELECT COUNT(*) AS TotalEspecialidades FROM Especialidades;
END;