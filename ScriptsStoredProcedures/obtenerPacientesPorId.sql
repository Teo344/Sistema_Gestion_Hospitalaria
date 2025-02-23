CREATE PROCEDURE uspObtenerPacientePorId
    @Id INT
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion
    FROM Pacientes
    WHERE Id = @Id;
END;
