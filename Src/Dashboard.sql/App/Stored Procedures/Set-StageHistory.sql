CREATE PROCEDURE [App].[Set-StageHistory]
    @StageHistoryId int,
    @StartDate date NULL,
    @CompletedDate date NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRAN;

    IF( EXISTS (SELECT * FROM [AppDbo].[StageHistory] WHERE [StageHistoryId] = @StageHistoryId) )
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('StageHistory ID does not exist', 17, 1);
        RETURN;
    END

    UPDATE  [AppDbo].[StageHistory]
    SET     [StartDate] = @StartDate,
            [CompletedDate] = @CompletedDate
    WHERE   [StageHistoryId] = @StageHistoryId;
    
    COMMIT TRAN;
END