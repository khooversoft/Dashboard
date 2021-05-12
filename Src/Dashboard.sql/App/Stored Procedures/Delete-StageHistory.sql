CREATE PROCEDURE [App].[Delete-StageHistory]
    @Provider nvarchar(50),
    @Stage nvarchar(100)
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

    DELETE  [AppDbo].[StageHistory]
    WHERE   [ProviderId] = @ProviderId
    AND     [StageId] = @StageId;
    
    COMMIT TRAN;
END