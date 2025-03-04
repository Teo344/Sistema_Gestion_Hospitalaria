USE [HospitalDB]
GO

INSERT INTO [dbo].[Facturacion3]
           ([TratamientoId]
           ,[MontoTotal]
           ,[MetodoPago]
           ,[FechaPago]
           ,[hbHabilitado])
     VALUES
           (<TratamientoId, int,>
           ,<MontoTotal, decimal(10,2),>
           ,<MetodoPago, varchar(50),>
           ,<FechaPago, date,>
           ,<hbHabilitado, int,>)
GO


