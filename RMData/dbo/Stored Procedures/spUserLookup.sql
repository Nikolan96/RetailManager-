CREATE PROCEDURE [dbo].[spUserLookup]
	@id nvarchar(128)

AS
Begin
	set nocount on;

	SELECT ID, FirstName, LastName, EmailAddress, CreatedDate
	From dbo.[User]
	WHERE ID = @id;
End
