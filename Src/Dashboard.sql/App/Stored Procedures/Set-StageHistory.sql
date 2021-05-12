CREATE PROCEDURE [App].[Set-StageHistory]
    @Provider nvarchar(50),
    @Stage nvarchar(100),
    @StartDate date NULL,
    @CompletedDate date NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRAN;

    DECLARE @ProviderId int = (SELECT [ProviderId] FROM [AppDbo].[Provider] WHERE [Provider] = @Provider);
    IF( @ProviderId IS NULL )
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('Provider %s does not exist', 17, 1, @ProviderId);
        RETURN;
    END

    DECLARE @StageId int = (SELECT [StageId] FROM [AppDbo].[Stage] WHERE [Stage] = @Stage);
    IF( @StageId IS NULL )
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('Stage %s does not exist', 17, 1, @StageId);
        RETURN;
    END

    MERGE   [AppDbo].[StageHistory] AS target
    USING   (SELECT @ProviderId, @StageId, @StartDate, @CompletedDate) AS source ([ProviderId], [StageId], [StartDate], [CompletedDate])
    ON      (target.[ProviderId] = source.[ProviderId] AND target.[StageId] = source.[StageId])
    WHEN MATCHED THEN
        UPDATE SET  [StartDate] = source.[StartDate]
                    ,[CompletedDate] = source.[CompletedDate]
    WHEN NOT MATCHED THEN
        INSERT ([ProviderId], [StageId], [StartDate], [CompletedDate])
        VALUES (source.[ProviderId], source.[StageId], source.[StartDate], source.[CompletedDate]);
    
    COMMIT TRAN;
END