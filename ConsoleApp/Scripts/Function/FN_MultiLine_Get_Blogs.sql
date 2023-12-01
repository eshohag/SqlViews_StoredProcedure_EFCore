
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FN_MultiLine_Get_Blogs]
(
	-- Add the parameters for the function here
	@blog_Id int
)
RETURNS @BlogInfo TABLE 
(
	-- Add the column definitions for the TABLE variable here
	BlogName VARCHAR(MAX),
	[URL] VARCHAR(250)
)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	INSERT INTO @BlogInfo
	SELECT  b.[Name] AS BlogName, b.[Url] AS [URL]
    FROM Blogs b
    WHERE b.BlogId=@blog_Id
	RETURN 
END
