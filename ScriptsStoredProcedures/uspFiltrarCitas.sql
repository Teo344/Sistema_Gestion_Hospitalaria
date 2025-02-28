CREATE PROCEDURE uspFiltrarCitas
    @PacienteId INT = NULL,
    @MedicoId INT = NULL,
    @FechaHora DATETIME = NULL,
    @Estado NVARCHAR(50) = NULL
AS
BEGIN
    SELECT * FROM Citas
    WHERE (@PacienteId IS NULL OR PacienteId = @PacienteId)
      AND (@MedicoId IS NULL OR MedicoId = @MedicoId)
      AND (@FechaHora IS NULL OR FechaHora = @FechaHora)
      AND (@Estado IS NULL OR Estado LIKE '%' + @Estado + '%');
END;