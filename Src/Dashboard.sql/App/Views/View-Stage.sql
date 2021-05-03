CREATE VIEW [App].[View-Stage]
AS
    SELECT  x.[StageId]
            ,x.[Stage]
            ,x.[OrderNumber]
    FROM    [AppDbo].[Stage] x;
