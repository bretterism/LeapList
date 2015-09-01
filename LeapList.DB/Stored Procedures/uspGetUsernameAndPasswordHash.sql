CREATE PROCEDURE [dbo].[uspGetUsernameAndPasswordHash]
	@username VARCHAR(255)
AS
	SELECT Username, PasswordHash
	FROM Profile
	WHERE Username = @username
RETURN 0