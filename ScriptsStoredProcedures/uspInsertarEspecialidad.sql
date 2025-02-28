CREATE PROCEDURE uspInsertarEspecialidad
    @Nombre NVARCHAR(100)
AS
BEGIN
    INSERT INTO Especialidades (Nombre)
    VALUES (@Nombre);
END;