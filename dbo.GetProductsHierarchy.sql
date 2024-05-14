CREATE PROCEDURE GetProductsHierarchyAsString
    @ProductId INT
AS
BEGIN
    DECLARE @ProductHierarchyString NVARCHAR(MAX);

    SET @ProductHierarchyString = '';

    SELECT @ProductHierarchyString = ISNULL(GroupHierarchy.Name, '') + ISNULL('/' + p.Name, '')
    FROM Products p
    LEFT JOIN (
        SELECT pg1.Id, 
               pg1.Name + ISNULL('/' + pg2.Name, '') AS Name
        FROM ProductGroups pg1
        LEFT JOIN ProductGroups pg2 ON pg1.ParentId = pg2.Id
    ) AS GroupHierarchy ON p.GroupId = GroupHierarchy.Id
    WHERE p.Id = @ProductId;

    SELECT @ProductHierarchyString AS ProductHierarchy;
END
