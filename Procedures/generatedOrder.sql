CREATE PROCEDURE GenerateOrder
    @UserId INT,
    @OrderId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
    BEGIN
        SET @OrderId = 0;
        RETURN;
    END

    DECLARE @BasketItems TABLE (Id INT, ProductId INT, Amount INT);
    INSERT INTO @BasketItems (Id, ProductId, Amount)
    SELECT Id, ProductId, Amount
    FROM BasketPositions
    WHERE UserId = @UserId;

    IF NOT EXISTS (SELECT 1 FROM @BasketItems)
    BEGIN
        SET @OrderId = 0;
        RETURN;
    END

    INSERT INTO Orders (UserId, Date, IsPaid)
    VALUES (@UserId, GETDATE(), 0);

    SET @OrderId = SCOPE_IDENTITY();

    DECLARE @ProductId INT, @Amount INT, @Price DECIMAL(18, 2);
    DECLARE BasketCursor CURSOR FOR
    SELECT ProductId, Amount
    FROM @BasketItems;

    OPEN BasketCursor;
    FETCH NEXT FROM BasketCursor INTO @ProductId, @Amount;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @Price = Price
        FROM Products
        WHERE Id = @ProductId;

        INSERT INTO OrderPositions (OrderId, ProductId, Amount, Price)
        VALUES (@OrderId, @ProductId, @Amount, @Price);

        FETCH NEXT FROM BasketCursor INTO @ProductId, @Amount;
    END

    CLOSE BasketCursor;
    DEALLOCATE BasketCursor;

    DELETE FROM BasketPositions
    WHERE UserId = @UserId
    AND Id IN (SELECT Id FROM @BasketItems);

    RETURN;
END;
