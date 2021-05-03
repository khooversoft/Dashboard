CREATE PROCEDURE [App].[Delete-Engagement]
    @EngagementId int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE  [AppDbo].[Engagement]
    WHERE   [EngagementId] = @EngagementId;

END