USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspInsertarCita]
    @PacienteIdentificacion NVARCHAR(15),
    @MedicoIdentificacion NVARCHAR(15),
    @FechaHora DATETIME,
    @Estado NVARCHAR(20)
AS
BEGIN
    DECLARE @PacienteId INT, @MedicoId INT;

    -- Obtener el Id del Paciente basado en la Identificacion
    SELECT @PacienteId = Id FROM Pacientes WHERE Identificacion = @PacienteIdentificacion;

    -- Obtener el Id del Médico basado en la Identificacion
    SELECT @MedicoId = Id FROM Medicos WHERE Identificacion = @MedicoIdentificacion;

    -- Insertar la nueva cita
    INSERT INTO Citas (PacienteId, MedicoId, FechaHora, Estado)
    VALUES (@PacienteId, @MedicoId, @FechaHora, @Estado);
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspActualizarCita]
    @Id INT,
    @PacienteIdentificacion NVARCHAR(15),
    @MedicoIdentificacion NVARCHAR(15),
    @FechaHora DATETIME,
    @Estado NVARCHAR(20)
AS
BEGIN
    DECLARE @PacienteId INT, @MedicoId INT;

    -- Obtener el Id del Paciente basado en la Identificacion
    SELECT @PacienteId = Id FROM Pacientes WHERE Identificacion = @PacienteIdentificacion;

    -- Obtener el Id del Médico basado en la Identificacion
    SELECT @MedicoId = Id FROM Medicos WHERE Identificacion = @MedicoIdentificacion;

    -- Actualizar la cita
    UPDATE Citas
    SET PacienteId = @PacienteId,
        MedicoId = @MedicoId,
        FechaHora = @FechaHora,
        Estado = @Estado
    WHERE Id = @Id;
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspEliminarCita]
    @Id INT
AS
BEGIN
    DELETE FROM Citas WHERE Id = @Id;
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspObtenerCitas]
AS
BEGIN
    SELECT 
        C.Id,
		P.Id AS PacienteId,
        P.Identificacion AS PacienteIdentificacion,
        M.Id AS MedicoId,
		M.Identificacion AS MedicoIdentificacion,
        C.FechaHora,
        C.Estado
    FROM Citas C
    INNER JOIN Pacientes P ON C.PacienteId = P.Id
    INNER JOIN Medicos M ON C.MedicoId = M.Id;
END;
GO

USE [HospitalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspFiltrarCitas]
    @PacienteIdentificacion NVARCHAR(15) = NULL,
    @MedicoIdentificacion NVARCHAR(15) = NULL,
    @FechaInicio DATETIME = NULL,
    @FechaFin DATETIME = NULL,
    @Estado NVARCHAR(20) = NULL
AS
BEGIN
    SELECT 
        C.Id,
		P.Id AS PacienteId,
        P.Identificacion AS PacienteIdentificacion,
        P.Nombre AS PacienteNombre,
        P.Apellido AS PacienteApellido,
		M.Id AS MedicoId,
        M.Identificacion AS MedicoIdentificacion,
        M.Nombre AS MedicoNombre,
        M.Apellido AS MedicoApellido,
        C.FechaHora,
        C.Estado
    FROM Citas C
    INNER JOIN Pacientes P ON C.PacienteId = P.Id
    INNER JOIN Medicos M ON C.MedicoId = M.Id
    WHERE (@PacienteIdentificacion IS NULL OR P.Identificacion = @PacienteIdentificacion)
      AND (@MedicoIdentificacion IS NULL OR M.Identificacion = @MedicoIdentificacion)
      AND (@FechaInicio IS NULL OR C.FechaHora >= @FechaInicio)
      AND (@FechaFin IS NULL OR C.FechaHora <= @FechaFin)
      AND (@Estado IS NULL OR C.Estado = @Estado);
END;
GO

CREATE PROCEDURE [dbo].[uspRecuperarCita]
    @Id INT
AS
BEGIN
    SELECT 
        c.Id AS Id, 
        p.Id AS PacienteId,
        p.Identificacion AS PacienteIdentificacion,
		m.Id AS MedicoId,
        m.Identificacion AS MedicoIdentificacion,
        c.FechaHora,
        c.Estado
    FROM Citas c
    INNER JOIN Pacientes p ON c.PacienteId = p.Id
	INNER JOIN Medicos M ON c.MedicoId = m.Id
    WHERE c.Id = @Id;
END;