CREATE TABLE [AppDbo].[StageHistory] (
    [StageHistoryId] BIGINT IDENTITY (1, 1) NOT NULL,
    [EngagementId]   INT    NOT NULL,
    [StageId]        INT    NOT NULL,
    [StartDate]      DATE   NULL,
    [CompletedDate]  DATE   NULL,
    CONSTRAINT [PK_StageHistory] PRIMARY KEY CLUSTERED ([StageHistoryId] ASC),
    CONSTRAINT [FK_StageHistory_Engagement] FOREIGN KEY ([EngagementId]) REFERENCES [AppDbo].[Engagement] ([EngagementId]) ON DELETE CASCADE,
    CONSTRAINT [FK_StageHistory_Stage] FOREIGN KEY ([StageId]) REFERENCES [AppDbo].[Stage] ([StageId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StageHistory]
    ON [AppDbo].[StageHistory]([EngagementId] ASC, [StageId] ASC);

