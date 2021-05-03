CREATE PROCEDURE [App].[Delete-Provider]
    @Provider nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE  [AppDbo].[Provider]
    WHERE   [Provider] = @Provider;

END
