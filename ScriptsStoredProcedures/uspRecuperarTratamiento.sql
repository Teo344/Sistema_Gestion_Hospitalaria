CREATE PROCEDURE uspRecuperarTratamiento
    @Id INT
AS
BEGIN
    SELECT * FROM Tratamientos WHERE Id = @Id;
END;