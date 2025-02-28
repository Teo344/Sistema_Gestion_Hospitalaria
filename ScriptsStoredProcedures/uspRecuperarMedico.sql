CREATE PROCEDURE uspRecuperarMedico
    @Id INT
AS
BEGIN
    SELECT * FROM Medicos WHERE Id = @Id;
END;