CREATE PROCEDURE uspFiltrarMedicos
    @Nombre NVARCHAR(100) = NULL,
    @Apellido NVARCHAR(100) = NULL,
    @EspecialidadId INT = NULL,
    @Activo BIT = NULL
AS
BEGIN
    SELECT * FROM Medicos
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Apellido IS NULL OR Apellido LIKE '%' + @Apellido + '%')
      AND (@EspecialidadId IS NULL OR EspecialidadId = @EspecialidadId)
      AND (@Activo IS NULL OR Activo = @Activo);
END;