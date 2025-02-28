CREATE PROCEDURE uspActualizarMedico
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @EspecialidadId INT,
    @Telefono NVARCHAR(15),
    @Email NVARCHAR(100),
    @Identificacion NVARCHAR(20),
    @Activo BIT
AS
BEGIN
    UPDATE Medicos
    SET Nombre = @Nombre, Apellido = @Apellido, EspecialidadId = @EspecialidadId,
        Telefono = @Telefono, Email = @Email, Identificacion = @Identificacion, Activo = @Activo
    WHERE Id = @Id;
END;