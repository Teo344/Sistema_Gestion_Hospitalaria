CREATE PROCEDURE uspEliminarAdministrador
    @Id INT
AS
BEGIN
    DELETE FROM Administradores WHERE Id = @Id;
END;