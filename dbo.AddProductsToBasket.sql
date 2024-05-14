CREATE PROCEDURE AddProductToBasket
    @ProductId INT,
    @BasketId INT
AS
BEGIN
    DECLARE @ProductIsActive BIT;
    DECLARE @ExistingBasketPositionAmount INT;

    SELECT @ProductIsActive = IsActive
    FROM Products
    WHERE Id = @ProductId;

    IF @ProductIsActive = 1
    BEGIN
        SELECT @ExistingBasketPositionAmount = Amount
        FROM BasketPositions
        WHERE ProductId = @ProductId AND Id = @BasketId;

        IF @@ROWCOUNT > 0
        BEGIN
            UPDATE BasketPositions
            SET Amount = @ExistingBasketPositionAmount + 1
            WHERE ProductId = @ProductId AND Id = @BasketId;
        END
        ELSE
        BEGIN
            INSERT INTO BasketPositions (ProductId, Id, Amount)
            VALUES (@ProductId, @BasketId, 1);
        END

        SELECT 1 AS Success;
    END
    ELSE
    BEGIN
        SELECT 0 AS Success; 
    END
END;
