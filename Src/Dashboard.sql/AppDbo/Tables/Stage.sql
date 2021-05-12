CREATE TABLE [AppDbo].[Stage] (
    [StageId]     INT            IDENTITY (1, 1) NOT NULL,
    [Stage]       NVARCHAR (100) NOT NULL,
    [OrderNumber] INT            NOT NULL,
    CONSTRAINT [PK_Stage] PRIMARY KEY CLUSTERED ([StageId] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_Stage]
    ON [AppDbo].[Stage]([Stage] ASC);

