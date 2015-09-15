-----------------------------------------------------------
-- Stored Procedure: uspGetSearchVMByProfileId
-- Parameters: A Profile ID
-- Returns: The SearchVM results for that Profile.
-----------------------------------------------------------

CREATE PROCEDURE [dbo].[uspGetAddEditSearchVMByProfileId]
	@profileId int
AS
	SELECT	sc.SearchId, 
			sc.SearchText, 
			cat.Category, 
			sc.MinPrice, 
			sc.MaxPrice
	FROM	SearchCriteria sc
	JOIN	CategorySearch cat ON cat.SearchId = sc.SearchId
	WHERE	sc.ProfileId = @profileId;