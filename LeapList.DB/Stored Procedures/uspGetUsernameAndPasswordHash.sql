CREATE PROCEDURE [uspGetUsernameAndPasswordHash]
	@username VARCHAR(255)
AS
	SELECT Username, PasswordHash
	FROM UserProfile
	WHERE Username = @username
RETURN 0