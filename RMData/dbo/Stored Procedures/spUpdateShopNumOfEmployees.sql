CREATE PROCEDURE [dbo].[spUpdateShopNumOfEmployees]

	@NumOfEmployees int,
	@ID int 

AS
begin
	Update Shop 
	set NumOfEmployees = @NumOfEmployees
	where ID = @ID;
end
