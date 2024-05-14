CREATE TRIGGER [dbo].[PreventDeactivation]
ON [dbo].[Products]
INSTEAD OF DELETE, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF @@ROWCOUNT > 0
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM deleted d
            WHERE EXISTS (
                SELECT 1
                FROM [dbo].[OrderPosition] op
                JOIN [dbo].[Order] o ON op.OrderID = o.ID
                WHERE op.ProductID = d.ID AND o.IsPaid = 0
            ) OR EXISTS (
                SELECT 1
                FROM [dbo].[BasketPosition] bp
                WHERE bp.ProductID = d.ID
            )
        )
        BEGIN
            RAISERROR ('Nie można usunąć/deaktywować produktu',16,1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        ELSE
        BEGIN
            IF EXISTS (SELECT * FROM inserted)
            BEGIN
                UPDATE p
                SET p.IsActive = i.IsActive
                FROM [dbo].[Products] p
                INNER JOIN inserted i ON p.ID = i.ID;
            END
        END
    END
END;
