CREATE TABLE [dbo].[CategorySearch] (
    [CategoryId] INT            IDENTITY (1, 1) NOT NULL,
    [SearchId]   INT            NOT NULL,
    [Category]   NVARCHAR (3)   NOT NULL,
    [SearchLink] NVARCHAR (MAX) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.CategorySearch] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_SearchId]
    ON [dbo].[CategorySearch]([SearchId] ASC);

