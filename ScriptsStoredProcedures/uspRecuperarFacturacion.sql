CREATE PROCEDURE uspRecuperarFacturacion
    @Id INT
AS
BEGIN
    SELECT * FROM Facturacion WHERE Id = @Id;
END;