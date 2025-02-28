CREATE PROCEDURE uspFiltrarFacturacion
    @PacienteId INT = NULL,
    @MetodoPago NVARCHAR(50) = NULL,
    @FechaPago DATE = NULL
AS
BEGIN
    SELECT * FROM Facturacion
    WHERE (@PacienteId IS NULL OR PacienteId = @PacienteId)
      AND (@MetodoPago IS NULL OR MetodoPago LIKE '%' + @MetodoPago + '%')
      AND (@FechaPago IS NULL OR FechaPago = @FechaPago);
END;