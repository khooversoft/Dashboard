CREATE PROCEDURE [App].[Set-Stage]
    @Stage nvarchar(50),
    @OrderNumber int
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRAN;

    DECLARE @Id int = (SELECT [StageId] FROM [AppDbo].[Stage] WHERE [Stage] = @Stage);

    IF( @Id IS NOT NULL )
    BEGIN
        UPDATE [AppDbo].[Stage]
        SET     [OrderNumber] = @OrderNumber
        WHERE   [Stage] = @Stage;
    END ELSE BEGIN
        INSERT INTO [AppDbo].[Stage] ([Stage], [OrderNumber])
        VALUES (@Stage, @OrderNumber);

        SET @Id = @@IDENTITY;
    END

    COMMIT TRAN;

    SELECT @Id as 'Id';
END