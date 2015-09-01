CREATE TABLE [dbo].[CLItem] (
    [ItemId]      INT             IDENTITY (1, 1) NOT NULL,
    [SearchId]    INT             NOT NULL,
    [Title]       NVARCHAR (MAX)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Link]        NVARCHAR (MAX)  NULL,
    [Date]        DATETIME        NOT NULL,
    CONSTRAINT [PK_dbo.CLItem] PRIMARY KEY CLUSTERED ([ItemId] ASC),
    CONSTRAINT [FK_dbo.CLItem_dbo.SearchCriteria_SearchId] FOREIGN KEY ([SearchId]) REFERENCES [dbo].[SearchCriteria] ([SearchId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SearchId]
    ON [dbo].[CLItem]([SearchId] ASC);

