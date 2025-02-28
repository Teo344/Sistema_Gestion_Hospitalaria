CREATE PROCEDURE uspActualizarCita
    @Id INT,
    @PacienteId INT,
    @MedicoId INT,
    @FechaHora DATETIME,
    @Estado NVARCHAR(50)
AS
BEGIN
    UPDATE Citas
    SET PacienteId = @PacienteId, MedicoId = @MedicoId, FechaHora = @FechaHora, Estado = @Estado
    WHERE Id = @Id;
END;