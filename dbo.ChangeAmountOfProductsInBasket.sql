CREATE PROCEDURE ChangeAmountOfProductsInBasket
    @BasketPositionId INT,
    @Amount INT
AS
BEGIN
    IF @Amount > 0
    BEGIN
        UPDATE BasketPositions
        SET Amount = @Amount
        WHERE Id = @BasketPositionId;
    END
    ELSE
    BEGIN
        RAISERROR ('Ilość musi być większa niż 0.', 16, 1);
    END;
END;
