CREATE PROCEDURE [App].[Set-Engagement]
    @EngagementId int,
    @Description nvarchar(1000) NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRAN;

    IF( EXISTS (SELECT * FROM [AppDbo].[Engagement] WHERE [EngagementId] = @EngagementId) )
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('Engagement ID does not exist', 17, 1);
        RETURN;
    END

    UPDATE  [AppDbo].[Engagement]
    SET     [Description] = @Description
    WHERE   [EngagementId] = @EngagementId;

    COMMIT TRAN;
END