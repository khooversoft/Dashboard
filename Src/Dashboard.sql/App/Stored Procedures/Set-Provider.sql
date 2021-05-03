CREATE PROCEDURE [App].[Set-Provider]
    @Provider nvarchar(50),
    @Show bit
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRAN;

    DECLARE @Id int = (SELECT [ProviderId] FROM [AppDbo].[Provider] WHERE [Provider] = @Provider);

    IF( @Id IS NOT NULL )
    BEGIN
        UPDATE [AppDbo].[Provider]
        SET     [Show] = @Show
        WHERE   [Provider] = @Provider;
    END ELSE BEGIN
        INSERT INTO [AppDbo].[Provider] ([Provider], [Show])
        VALUES (@Provider, @Show);

        SET @Id = @@IDENTITY;
    END

    COMMIT TRAN;

    SELECT @Id as 'Id';
END