CREATE VIEW [App].[View-StageHistory]
AS
    SELECT  x.[StageHistoryId]
            ,x.[EngagementId]
            ,x.[StageId]
            ,x.[StartDate]
            ,x.[CompletedDate]
    FROM    [AppDbo].[StageHistory] x
;
