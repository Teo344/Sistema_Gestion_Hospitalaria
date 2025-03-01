CREATE PROCEDURE uspRecuperarEspecialidad
    @Id INT
AS
BEGIN
    SELECT 
	Id,
	Nombre
	FROM Especialidades WHERE Id = @Id;
END;