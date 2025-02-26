CREATE PROCEDURE uspFiltrarEspecialidades
    @Nombre NVARCHAR(100) = NULL
AS
BEGIN
    SELECT * FROM Especialidades
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%');
END;