CREATE TYPE [dbo].[UpdateCategories] AS TABLE (
    [Category]       CHAR (3)      NOT NULL,
    [InsertOrDelete] CHAR (1)      NOT NULL,
    [SearchLink]     VARCHAR (MAX) NOT NULL);

