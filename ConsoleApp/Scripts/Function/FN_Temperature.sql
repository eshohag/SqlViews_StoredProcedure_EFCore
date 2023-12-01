
/* =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>

SELECT [dbo].[FN_Temperature](30) AS Fahrenheit
-- =============================================*/
CREATE FUNCTION [dbo].[FN_Temperature](
	@Celcius int
)
RETURNS real
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Fahrenheit real
	SET @Fahrenheit=(@Celcius*9/5)+32
	RETURN @Fahrenheit;
END


