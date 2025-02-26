CREATE PROCEDURE uspActualizarFacturacion
    @Id INT,
    @PacienteId INT,
    @Monto DECIMAL(10,2),
    @MetodoPago NVARCHAR(50),
    @FechaPago DATE
AS
BEGIN
    UPDATE Facturacion
    SET PacienteId = @PacienteId, Monto = @Monto, MetodoPago = @MetodoPago, FechaPago = @FechaPago
    WHERE Id = @Id;
END;