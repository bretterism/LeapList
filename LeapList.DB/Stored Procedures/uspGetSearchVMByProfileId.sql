-----------------------------------------------------------
-- Stored Procedure: uspGetSearchVMByProfileId
-- Parameters: A Profile ID
-- Returns: The SearchVM results for that Profile.
-----------------------------------------------------------

CREATE PROCEDURE [dbo].[uspGetSearchVMByProfileId]
	@profileId int
AS
	SELECT	sc.SearchId, 
			sc.SearchText, 
			cat.Category, 
			sc.MinPrice, 
			sc.MaxPrice
	FROM	SearchCriteria sc
	JOIN	SC_Category cat ON cat.SearchId = sc.SearchId
	WHERE	sc.ProfileId = @profileId;