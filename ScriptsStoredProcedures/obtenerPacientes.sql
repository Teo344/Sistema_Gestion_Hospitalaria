CREATE PROCEDURE uspObtenerPacientes
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion
    FROM Pacientes;
END;
