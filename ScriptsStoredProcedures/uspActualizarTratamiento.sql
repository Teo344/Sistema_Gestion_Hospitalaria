CREATE PROCEDURE uspActualizarTratamiento
    @Id INT,
    @PacienteId INT,
    @Descripcion NVARCHAR(255),
    @Fecha DATE,
    @Costo DECIMAL(10,2)
AS
BEGIN
    UPDATE Tratamientos
    SET PacienteId = @PacienteId, Descripcion = @Descripcion, Fecha = @Fecha, Costo = @Costo
    WHERE Id = @Id;
END;