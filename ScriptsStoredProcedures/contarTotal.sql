USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspObtenerTotalPacientes]
AS
BEGIN
    SELECT COUNT(*) AS TotalPacientes FROM Pacientes;
END;


CREATE PROCEDURE uspObtenerTotalEspecialidades
AS
BEGIN
    SELECT COUNT(*) AS TotalEspecialidades FROM Especialidades;
END;

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[uspObtenerTotalMedicos]
AS
BEGIN
    SELECT COUNT(*) AS TotalMedicos FROM Medicos;
END;

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspObtenerTotalCitas]
AS
BEGIN
    SELECT COUNT(*) AS TotalCitas FROM Citas;
END;

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspCalcularIngresoTotal]
AS
BEGIN
    SELECT SUM(Monto) AS IngresoTotal
    FROM Facturacion;
END;


USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[uspCalcularIngresoMesActual]
AS
BEGIN
    SELECT SUM(Monto) AS IngresoMensual
    FROM Facturacion
    WHERE YEAR(FechaPago) = YEAR(GETDATE())
    AND MONTH(FechaPago) = MONTH(GETDATE());
END;
