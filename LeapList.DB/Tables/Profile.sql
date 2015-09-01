CREATE TABLE [dbo].[Profile] (
    [ProfileId]    INT            IDENTITY (1, 1) NOT NULL,
    [City]         NVARCHAR (MAX) NULL,
    [Username]     NVARCHAR (MAX) NULL,
    [PasswordHash] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Profile] PRIMARY KEY CLUSTERED ([ProfileId] ASC)
);

