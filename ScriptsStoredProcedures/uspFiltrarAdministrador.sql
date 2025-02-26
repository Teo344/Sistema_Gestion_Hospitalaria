CREATE PROCEDURE uspFiltrarAdministrador
    @Nombre NVARCHAR(100) = NULL,
    @Apellido NVARCHAR(100) = NULL,
    @Email NVARCHAR(255) = NULL
AS
BEGIN
    SELECT * FROM Administradores
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Apellido IS NULL OR Apellido LIKE '%' + @Apellido + '%')
      AND (@Email IS NULL OR Email LIKE '%' + @Email + '%');
END;