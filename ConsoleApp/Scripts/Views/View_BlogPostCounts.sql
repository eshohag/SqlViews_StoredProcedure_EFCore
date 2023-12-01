CREATE VIEW View_BlogPostCounts AS
SELECT b.Name, Count(p.PostId) as PostCount
FROM Blogs b
JOIN Posts p on p.BlogId = b.BlogId
GROUP BY b.Name;