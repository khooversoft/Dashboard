CREATE TABLE [AppDbo].[StageHistory] (
    [StageHistoryId] BIGINT IDENTITY (1, 1) NOT NULL,
    [ProviderId]     INT    NOT NULL,
    [StageId]        INT    NOT NULL,
    [StartDate]      DATE   NULL,
    [CompletedDate]  DATE   NULL,
    CONSTRAINT [PK_StageHistory] PRIMARY KEY CLUSTERED ([StageHistoryId] ASC),
    CONSTRAINT [FK_StageHistory_Provider] FOREIGN KEY ([ProviderId]) REFERENCES [AppDbo].[Provider] ([ProviderId]),
    CONSTRAINT [FK_StageHistory_Stage] FOREIGN KEY ([StageId]) REFERENCES [AppDbo].[Stage] ([StageId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StageHistory]
    ON [AppDbo].[StageHistory]([ProviderId] ASC, [StageId] ASC);



