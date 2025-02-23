CREATE PROCEDURE uspEliminarPaciente
    @Id INT
AS
BEGIN
    DELETE FROM Pacientes WHERE Id = @Id;
END;
