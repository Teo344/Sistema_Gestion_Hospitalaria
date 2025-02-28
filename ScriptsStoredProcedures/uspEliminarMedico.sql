CREATE PROCEDURE uspEliminarMedico
    @Id INT
AS
BEGIN
    DELETE FROM Medicos WHERE Id = @Id;
END;