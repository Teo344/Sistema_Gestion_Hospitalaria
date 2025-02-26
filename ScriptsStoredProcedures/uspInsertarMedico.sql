CREATE PROCEDURE uspInsertarMedico
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @EspecialidadId INT,
    @Telefono NVARCHAR(15),
    @Email NVARCHAR(100),
    @Identificacion NVARCHAR(20),
    @Activo BIT
AS
BEGIN
    INSERT INTO Medicos (Nombre, Apellido, EspecialidadId, Telefono, Email, Identificacion, Activo)
    VALUES (@Nombre, @Apellido, @EspecialidadId, @Telefono, @Email, @Identificacion, @Activo);
END;