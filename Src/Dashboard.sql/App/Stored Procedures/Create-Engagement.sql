CREATE PROCEDURE [App].[Create-Engagement]
    @Provider nvarchar(50),
    @Description nvarchar(1000) NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRAN

    DECLARE @ProviderId int = (SELECT [ProviderId] FROM [AppDbo].[Provider] WHERE [Provider] = @Provider);
    IF( @ProviderId IS NULL )
    BEGIN
        ROLLBACK TRAN;
        RAISERROR('Provider does not exist', 17, 1);
        RETURN;
    END

    INSERT INTO [AppDbo].[Engagement] ([ProviderId], [Description])
    VALUES (@ProviderId, @Description);

    DECLARE @Id int = @@IDENTITY;

    INSERT INTO [AppDbo].[StageHistory] ([EngagementId], [StageId])
    SELECT  @Id
            ,x.[StageId]
    FROM    [AppDbo].[Stage] x;

    COMMIT TRAN;

    SELECT @Id as 'Id';
END