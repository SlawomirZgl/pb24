CREATE PROCEDURE GetFilteredProducts
    @orderBy NVARCHAR(50) = NULL,
    @orderAscending BIT = 1,
    @getNotActive BIT = 0,
    @filterByName NVARCHAR(100) = NULL,
    @filterByGroupName NVARCHAR(100) = NULL,
    @filterByGroupId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    WITH GroupHierarchyPaths AS (
        SELECT
            Id,
            ParentId,
            CAST(Name AS NVARCHAR(MAX)) AS FullGroupName -- Explicitly cast to NVARCHAR(MAX)
        FROM
            ProductGroups
        WHERE
            Id = @filterByGroupId OR @filterByGroupId IS NULL
        UNION ALL
        SELECT
            pg.Id,
            pg.ParentId,
            CAST(gh.FullGroupName + N' / ' + pg.Name AS NVARCHAR(MAX)) -- Explicitly cast to NVARCHAR(MAX)
        FROM
            ProductGroups AS pg
        INNER JOIN
            GroupHierarchyPaths AS gh ON pg.ParentId = gh.Id
    ),
    GroupHierarchy AS (
        SELECT
            Id,
            ParentId,
            FullGroupName
        FROM
            GroupHierarchyPaths
        WHERE
            NOT EXISTS (
                SELECT 1
                FROM GroupHierarchyPaths AS gh2
                WHERE gh2.Id = GroupHierarchyPaths.Id AND gh2.ParentId = GroupHierarchyPaths.ParentId AND gh2.FullGroupName > GroupHierarchyPaths.FullGroupName
            )
    )

    SELECT 
        p.Id, 
        p.Name, 
        p.Price, 
        p.Image,
        p.IsActive, 
        p.GroupId, 
        ISNULL(gh.FullGroupName, '') AS GroupName
    FROM 
        Products p
    LEFT JOIN 
        GroupHierarchy gh ON p.GroupId = gh.Id
    WHERE 
        (@getNotActive = 1 OR p.IsActive = 1)
        AND (@filterByName IS NULL OR p.Name LIKE '%' + @filterByName + '%')
        AND (@filterByGroupName IS NULL OR gh.FullGroupName LIKE '%' + @filterByGroupName + '%')
    ORDER BY
        CASE 
            WHEN @orderBy = 'Name' THEN p.Name
            WHEN @orderBy = 'Price' THEN p.Price
            WHEN @orderBy = 'GroupName' THEN ISNULL(gh.FullGroupName, '')
            ELSE p.Id
        END;
END
