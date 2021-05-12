CREATE VIEW [App].[View-StageHistory]
AS
    SELECT  x.[StageHistoryId]
            ,x.[ProviderId]
            ,p.[Provider]
            ,x.[StageId]
            ,s.[Stage]
            ,x.[StartDate]
            ,x.[CompletedDate]
    FROM    [AppDbo].[StageHistory] x
            INNER JOIN [AppDbo].[Provider] p on x.[ProviderId] = p.[ProviderId]
            INNER JOIN [AppDbo].[Stage] s on x.[StageId] = s.[StageId]
;
