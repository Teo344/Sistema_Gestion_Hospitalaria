CREATE PROCEDURE uspRecuperarCita
    @Id INT
AS
BEGIN
    SELECT * FROM Citas WHERE Id = @Id;
END;