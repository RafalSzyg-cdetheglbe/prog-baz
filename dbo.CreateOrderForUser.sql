CREATE PROCEDURE CreateOrderForUser
            @UserId INT
            AS
            BEGIN
                DECLARE @OrderId INT;
                INSERT INTO Orders (UserId, Date, IsPaid)
                VALUES (@UserId, GETDATE(), 0);

                SELECT @OrderId = SCOPE_IDENTITY();

                INSERT INTO OrderPositions (OrderId, ProductId)
                SELECT @OrderId, ProductId
                FROM BasketPositions
                WHERE UserId = @UserId;

                DELETE FROM BasketPositions WHERE UserId = @UserId;
            END