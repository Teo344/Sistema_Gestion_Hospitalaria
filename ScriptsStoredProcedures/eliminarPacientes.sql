USE [HospitalDB]
GO
/****** Object:  StoredProcedure [dbo].[uspEliminarPaciente]    Script Date: 24/2/2025 21:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[uspEliminarPaciente]
    @Id INT
AS
BEGIN
    DELETE FROM Pacientes WHERE Id = @Id;
END;