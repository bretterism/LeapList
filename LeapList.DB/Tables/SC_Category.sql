CREATE TABLE [dbo].[SC_Category] (
    [CategoryId] INT          IDENTITY (1, 1) NOT NULL,
    [SearchId]   INT          NOT NULL,
    [Category]   NVARCHAR (3) NOT NULL,
    CONSTRAINT [PK_dbo.SC_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC),
    CONSTRAINT [FK_dbo.SC_Category_dbo.SearchCriteria_SearchId] FOREIGN KEY ([SearchId]) REFERENCES [dbo].[SearchCriteria] ([SearchId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SearchId]
    ON [dbo].[SC_Category]([SearchId] ASC);

