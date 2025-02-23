CREATE PROCEDURE uspActualizarPaciente
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Telefono NVARCHAR(15),
    @Email NVARCHAR(100),
    @Direccion NVARCHAR(255)
AS
BEGIN
    UPDATE Pacientes
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        FechaNacimiento = @FechaNacimiento,
        Telefono = @Telefono,
        Email = @Email,
        Direccion = @Direccion
    WHERE Id = @Id;
END;
