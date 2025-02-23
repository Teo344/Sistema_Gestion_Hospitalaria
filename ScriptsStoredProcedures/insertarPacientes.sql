CREATE PROCEDURE uspInsertarPaciente
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Telefono NVARCHAR(15),
    @Email NVARCHAR(100),
    @Direccion NVARCHAR(255)
AS
BEGIN
    INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @Telefono, @Email, @Direccion);
END;
