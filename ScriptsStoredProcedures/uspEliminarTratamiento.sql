CREATE PROCEDURE uspEliminarTratamiento
    @Id INT
AS
BEGIN
    DELETE FROM Tratamientos WHERE Id = @Id;
END;