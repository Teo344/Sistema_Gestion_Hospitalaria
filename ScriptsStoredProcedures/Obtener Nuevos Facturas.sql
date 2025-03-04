CREATE PROCEDURE ObtenerFacturaciones
AS
BEGIN
    SELECT 
        f.Id, 
        p.Id AS PacienteId, 
        p.Nombre AS PacienteNombre, 
        p.Apellido AS PacienteApellido,
        p.Identificacion AS PacienteIdentificacion, 
        f.MontoTotal AS Monto, 
        f.MetodoPago, 
        f.FechaPago, 
        t.Id AS TratamientoId,
        t.Costo AS TratamientoCosto
    FROM Facturacion3 f
    INNER JOIN Tratamientos t ON f.TratamientoId = t.Id
    INNER JOIN Pacientes p ON t.PacienteId = p.Id
    WHERE f.hbHabilitado = 1;  -- Solo facturas habilitadas
END;
