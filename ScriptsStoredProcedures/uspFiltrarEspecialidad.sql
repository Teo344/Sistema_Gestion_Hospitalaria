CREATE PROCEDURE uspFiltrarEspecialidad
    @Nombre NVARCHAR(100) = NULL
AS
BEGIN

DECLARE @sql NVARCHAR(MAX)

    SET @sql = 'SELECT Id, Nombre 
                FROM Especialidades
                WHERE 1=1'

	IF @nombre <> ''
    SET @sql = @sql + ' AND Nombre LIKE ''%' + @nombre + '%'''


END;