CREATE PROCEDURE uspInsertarCita
    @PacienteId INT,
    @MedicoId INT,
    @FechaHora DATETIME,
    @Estado NVARCHAR(50)
AS
BEGIN
    INSERT INTO Citas (PacienteId, MedicoId, FechaHora, Estado)
    VALUES (@PacienteId, @MedicoId, @FechaHora, @Estado);
END;