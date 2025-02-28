CREATE PROCEDURE uspActualizarEspecialidad
    @Id INT,
    @Nombre NVARCHAR(100)
AS
BEGIN
    UPDATE Especialidades SET Nombre = @Nombre WHERE Id = @Id;
END;