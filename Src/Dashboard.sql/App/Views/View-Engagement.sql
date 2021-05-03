CREATE VIEW [App].[View-Engagement]
AS
    SELECT  x.[EngagementId]
            ,x.[ProviderId]
            ,p.[Provider]
            ,x.[Description]
    FROM    [AppDbo].[Engagement] x
            inner join [AppDbo].[Provider] p on x.[ProviderId] = p.[ProviderId]
;

