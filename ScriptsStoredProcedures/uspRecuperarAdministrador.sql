CREATE PROCEDURE uspRecuperarAdministrador
    @Id INT
AS
BEGIN
    SELECT * FROM Administradores WHERE Id = @Id;
END;