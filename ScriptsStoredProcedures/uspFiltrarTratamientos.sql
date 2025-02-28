CREATE PROCEDURE uspFiltrarTratamientos
    @PacienteId INT = NULL,
    @Fecha DATE = NULL
AS
BEGIN
    SELECT * FROM Tratamientos
    WHERE (@PacienteId IS NULL OR PacienteId = @PacienteId)
      AND (@Fecha IS NULL OR Fecha = @Fecha);
END;