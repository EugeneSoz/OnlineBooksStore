SELECT [s].[Id], 
[s].[Name] as Subcategory, 
[s].[ParentCategoryID], 
[s.ParentCategory].[Id], 
[s.ParentCategory].[Name] as Category, 
[s.ParentCategory].[ParentCategoryID],
case WHEN [s.ParentCategory].[Name] IS NULL THEN [s].[Name] ELSE [s.ParentCategory].[Name] + ' <=> ' +  [s].[Name] end as dn
FROM [Categories] AS [s]
LEFT JOIN [Categories] AS [s.ParentCategory] ON [s].[ParentCategoryID] = [s.ParentCategory].[Id]
ORDER BY (dn)