CREATE TABLE [dbo].[CLItem] (
    [ItemId]      INT             IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (MAX)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Link]        NVARCHAR (MAX)  NULL,
    [Date]        DATETIME        NOT NULL,
    [CategoryId]  INT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.CLItem] PRIMARY KEY CLUSTERED ([ItemId] ASC)
);




GO


