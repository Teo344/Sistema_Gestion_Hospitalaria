CREATE PROCEDURE uspEliminarFacturacion
    @Id INT
AS
BEGIN
    DELETE FROM Facturacion WHERE Id = @Id;
END;