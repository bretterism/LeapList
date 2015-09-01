CREATE TABLE [dbo].[SearchCriteria] (
    [SearchId]   INT             IDENTITY (1, 1) NOT NULL,
    [ProfileId]  INT             NOT NULL,
    [SearchText] NVARCHAR (MAX)  NULL,
    [MinPrice]   DECIMAL (18, 2) NULL,
    [MaxPrice]   DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_dbo.SearchCriteria] PRIMARY KEY CLUSTERED ([SearchId] ASC),
    CONSTRAINT [FK_dbo.SearchCriteria_dbo.Profile_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([ProfileId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProfileId]
    ON [dbo].[SearchCriteria]([ProfileId] ASC);

