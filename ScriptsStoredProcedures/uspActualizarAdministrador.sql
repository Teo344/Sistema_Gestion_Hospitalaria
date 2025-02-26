CREATE PROCEDURE uspActualizarAdministrador
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(255)
AS
BEGIN
    UPDATE Administradores
    SET Nombre = @Nombre, Apellido = @Apellido, Clave = @Clave, Email = @Email
    WHERE Id = @Id;
END;