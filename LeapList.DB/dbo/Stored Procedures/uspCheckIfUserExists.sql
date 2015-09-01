CREATE PROCEDURE [dbo].[uspCheckIfUserExists]
	@username VARCHAR(8000)
AS
	SELECT COUNT(*) AS UserCheck
	FROM Profile
	WHERE Username = @username