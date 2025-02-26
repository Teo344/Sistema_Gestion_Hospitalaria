CREATE PROCEDURE uspInsertarFacturacion
    @PacienteId INT,
    @Monto DECIMAL(10,2),
    @MetodoPago NVARCHAR(50),
    @FechaPago DATE
AS
BEGIN
    INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago)
    VALUES (@PacienteId, @Monto, @MetodoPago, @FechaPago);
END;