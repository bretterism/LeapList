CREATE PROCEDURE [dbo].[uspUpdateSearchCriteria]
	@searchId INT,
	@searchText VARCHAR(MAX),
	@minPrice DECIMAL(18, 2),
	@maxPrice DECIMAL(18, 2),
	@categories UpdateCategories READONLY
AS
	DECLARE @temp TABLE
	(
		searchId INT,
		category CHAR(3),
		searchLink VARCHAR(1000)
	);

	UPDATE
		SearchCriteria
	SET
		SearchText = @searchText,
		MinPrice = @minPrice,
		MaxPrice = @maxPrice
	WHERE
		SearchId = @searchId
	;
	
	INSERT INTO 
		@temp (category, searchLink)
		SELECT
			cat.Category, cat.SearchLink
		FROM @categories cat
		WHERE cat.InsertOrDelete = 'I'
	;
	
	UPDATE @temp
	SET searchId = @searchId
	;

	-- Insert the new categories selected
	INSERT INTO
		CategorySearch (SearchId, Category, SearchLink)
	SELECT *
	FROM @temp
	

	-- Delete the categories that have been unselected
	DELETE
	FROM
		CategorySearch
	WHERE
		Category = (SELECT cat.Category
					FROM @categories cat
					WHERE cat.InsertOrDelete = 'D') 
		AND
		SearchId = @searchId
	;

