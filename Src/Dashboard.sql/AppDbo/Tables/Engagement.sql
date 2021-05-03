CREATE TABLE [AppDbo].[Engagement] (
    [EngagementId] INT             IDENTITY (1, 1) NOT NULL,
    [ProviderId]   INT             NOT NULL,
    [Description]  NVARCHAR (1000) NULL,
    CONSTRAINT [PK_Engagement] PRIMARY KEY CLUSTERED ([EngagementId] ASC),
    CONSTRAINT [FK_Engagement_Provider] FOREIGN KEY ([ProviderId]) REFERENCES [AppDbo].[Provider] ([ProviderId]) ON DELETE CASCADE
);





