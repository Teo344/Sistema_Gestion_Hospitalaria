CREATE PROCEDURE uspEliminarEspecialidad
    @Id INT
AS
BEGIN
    DELETE FROM Especialidades WHERE Id = @Id;
END;