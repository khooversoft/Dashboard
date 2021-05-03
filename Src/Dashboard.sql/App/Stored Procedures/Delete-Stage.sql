CREATE PROCEDURE [App].[Delete-Stage]
    @Stage nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE  [AppDbo].[Stage]
    WHERE   [Stage] = @Stage;

END
