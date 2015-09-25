CREATE PROCEDURE [dbo].[uspGetAddEditSearchVMBySearchId] 
	@searchId INT
AS
	SELECT
		sc.SearchText,
		cat.Category, 
		sc.MinPrice, 
		sc.MaxPrice
	FROM 
		SearchCriteria sc
	JOIN
		CategorySearch cat ON sc.SearchId = cat.SearchId
	WHERE
		sc.SearchId = @searchId

