CREATE TABLE [AppDbo].[Provider] (
    [ProviderId] INT           IDENTITY (1, 1) NOT NULL,
    [Provider]   NVARCHAR (50) NOT NULL,
    [Show]       BIT           CONSTRAINT [DF_Provider_Show] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED ([ProviderId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Provider]
    ON [AppDbo].[Provider]([Provider] ASC);

