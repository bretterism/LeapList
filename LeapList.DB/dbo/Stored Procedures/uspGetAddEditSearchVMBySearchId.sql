
CREATE PROCEDURE uspGetAddEditSearchVMBySearchId 
	@searchId INT
AS
BEGIN
	SET NOCOUNT ON;
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
END