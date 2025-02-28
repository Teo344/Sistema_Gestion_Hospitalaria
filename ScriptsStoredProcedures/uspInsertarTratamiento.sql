CREATE PROCEDURE uspInsertarTratamiento
    @PacienteId INT,
    @Descripcion NVARCHAR(255),
    @Fecha DATE,
    @Costo DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Tratamientos (PacienteId, Descripcion, Fecha, Costo)
    VALUES (@PacienteId, @Descripcion, @Fecha, @Costo);
END;