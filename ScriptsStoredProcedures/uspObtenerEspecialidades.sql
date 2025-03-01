CREATE PROCEDURE uspObtenerEspecialidades
AS
BEGIN
    SELECT id,Nombre FROM Especialidades;
END;