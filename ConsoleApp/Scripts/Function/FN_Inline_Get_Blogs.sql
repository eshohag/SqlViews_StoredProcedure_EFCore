
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FN_Inline_Get_Blogs]
(	
	-- Add the parameters for the function here
	@blog_Id int
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT  *
    FROM Blogs b
    WHERE b.BlogId=@blog_Id
)
