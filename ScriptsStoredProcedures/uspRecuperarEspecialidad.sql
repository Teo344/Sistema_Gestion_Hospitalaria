CREATE PROCEDURE uspRecuperarEspecialidad
    @Id INT
AS
BEGIN
    SELECT * FROM Especialidades WHERE Id = @Id;
END;