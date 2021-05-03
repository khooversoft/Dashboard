CREATE VIEW [App].[View-Provider]
AS
    SELECT  x.[ProviderId]
            ,x.[Provider]
            ,x.[Show]
    FROM    [AppDbo].[Provider] x;
