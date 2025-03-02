USE [HospitalDB]
GO

-- Insertar Tratamiento
CREATE PROCEDURE [dbo].[uspInsertarTratamiento]
    @IdentificacionPaciente NVARCHAR(15),
    @Descripcion NVARCHAR(255),
    @Fecha DATE,
    @Costo DECIMAL(10,2)
AS
BEGIN
    DECLARE @PacienteId INT;

    -- Obtener el Id del paciente usando la Identificacion
    SELECT @PacienteId = Id FROM Pacientes WHERE Identificacion = @IdentificacionPaciente;

    IF @PacienteId IS NULL
    BEGIN
        RAISERROR('Paciente no encontrado.', 16, 1);
        RETURN;
    END

    INSERT INTO Tratamientos (PacienteId, Descripcion, Fecha, Costo)
    VALUES (@PacienteId, @Descripcion, @Fecha, @Costo);
END;
GO

-- Actualizar Tratamiento
CREATE PROCEDURE [dbo].[uspActualizarTratamiento]
    @Id INT,
    @IdentificacionPaciente NVARCHAR(15),
    @Descripcion NVARCHAR(255),
    @Fecha DATE,
    @Costo DECIMAL(10,2)
AS
BEGIN
    DECLARE @PacienteId INT;

    -- Obtener el Id del paciente usando la Identificacion
    SELECT @PacienteId = Id FROM Pacientes WHERE Identificacion = @IdentificacionPaciente;

    IF @PacienteId IS NULL
    BEGIN
        RAISERROR('Paciente no encontrado.', 16, 1);
        RETURN;
    END

    UPDATE Tratamientos
    SET PacienteId = @PacienteId,
        Descripcion = @Descripcion,
        Fecha = @Fecha,
        Costo = @Costo
    WHERE Id = @Id;
END;
GO

-- Eliminar Tratamiento
CREATE PROCEDURE [dbo].[uspEliminarTratamiento]
    @Id INT
AS
BEGIN
    DELETE FROM Tratamientos WHERE Id = @Id;
END;
GO

-- Obtener todos los tratamientos con información del paciente
CREATE PROCEDURE [dbo].[uspObtenerTratamientos]
AS
BEGIN
    SELECT 
        t.Id AS Id, -- Cambia 'Id_Tratamiento' a 'Id'
        p.Id AS PacienteId,
        p.Identificacion AS IdentificacionPaciente,
        t.Descripcion,
        t.Fecha,
        t.Costo
    FROM Tratamientos t
    INNER JOIN Pacientes p ON t.PacienteId = p.Id;
END;
GO
-- Buscar Tratamientos por Identificación del Paciente
CREATE PROCEDURE [dbo].[uspBuscarTratamientosPorIdentificacion]
    @IdentificacionPaciente NVARCHAR(15)
AS
BEGIN
    SELECT 
        t.Id AS Id, -- Cambia 'Id_Tratamiento' a 'Id'
        p.Id AS PacienteId,
        p.Identificacion AS IdentificacionPaciente,
        t.Descripcion,
        t.Fecha,
        t.Costo
    FROM Tratamientos t
    INNER JOIN Pacientes p ON t.PacienteId = p.Id
    WHERE p.Identificacion = @IdentificacionPaciente;
END;
GO

USE [HospitalDB]
GO

-- Recuperar un tratamiento específico por su ID
CREATE PROCEDURE [dbo].[uspRecuperarTratamiento]
    @Id INT
AS
BEGIN
    SELECT 
        t.Id AS Id, -- Cambia 'Id_Tratamiento' a 'Id'
        p.Id AS PacienteId,
        p.Identificacion AS IdentificacionPaciente,
        t.Descripcion,
        t.Fecha,
        t.Costo
    FROM Tratamientos t
    INNER JOIN Pacientes p ON t.PacienteId = p.Id
    WHERE t.Id = @Id;
END;
GO

-- Filtrar tratamientos por diferentes criterios
CREATE PROCEDURE [dbo].[uspFiltrarTratamientos]
    @IdentificacionPaciente NVARCHAR(15) = '',
    @Descripcion NVARCHAR(255) = '',
    @Fecha DATE = NULL,
    @Costo DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);

    SET @sql = '
        SELECT 
            t.Id AS Id, 
			p.Id AS PacienteId,
			p.Identificacion AS IdentificacionPaciente,
			t.Descripcion,
			t.Fecha,
			t.Costo
        FROM Tratamientos t
        INNER JOIN Pacientes p ON t.PacienteId = p.Id
        WHERE 1=1';

    IF @IdentificacionPaciente <> ''
        SET @sql = @sql + ' AND p.Identificacion LIKE ''%' + @IdentificacionPaciente + '%''';

    IF @Descripcion <> ''
        SET @sql = @sql + ' AND t.Descripcion LIKE ''%' + @Descripcion + '%''';

    IF @Fecha IS NOT NULL
        SET @sql = @sql + ' AND t.Fecha = ''' + CONVERT(NVARCHAR, @Fecha, 23) + '''';

    IF @Costo IS NOT NULL
        SET @sql = @sql + ' AND t.Costo = ' + CAST(@Costo AS NVARCHAR);

    EXEC sp_executesql @sql;
END;
GO
