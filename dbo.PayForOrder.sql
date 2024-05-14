CREATE PROCEDURE PayForOrder
    @OrderId INT,
    @AmountPaid DECIMAL(18, 2)
AS
BEGIN
    DECLARE @TotalAmount DECIMAL(18, 2);

    SELECT @TotalAmount = SUM(op.Amount * p.Price)
    FROM OrderPositions op
    INNER JOIN Products p ON op.ProductId = p.Id
    WHERE op.OrderId = @OrderId;

    UPDATE Orders
    SET IsPaid = CASE WHEN @AmountPaid >= @TotalAmount THEN 1 ELSE 0 END
    WHERE Id = @OrderId;
END
