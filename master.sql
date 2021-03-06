USE [master]
GO
/****** Object:  StoredProcedure [dbo].[pBackup]    Script Date: 14/10/2016 06:19:31 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pBackup] (@pLocalizacion nvarchar(max))
as
begin
BACKUP DATABASE InvernAr
TO DISK = @pLocalizacion
WITH Format
end


GO
/****** Object:  StoredProcedure [dbo].[pRestore]    Script Date: 14/10/2016 06:19:31 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pRestore] (@pLocalizacion nvarchar(max))
as
begin
ALTER DATABASE InvernAr 
SET SINGLE_USER WITH
ROLLBACK immediate
RESTORE DATABASE InvernAr 
FROM DISK = @pLocalizacion
WITH REPLACE
ALTER DATABASE InvernAr 
SET MULTI_USER
end


GO
