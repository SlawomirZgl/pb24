CREATE TRIGGER [dbo].[PreventDeactivation]
ON [dbo].[Products]
INSTEAD OF DELETE, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if any rows are affected by the operation
    IF @@ROWCOUNT > 0
    BEGIN
        -- Check if any product is being deleted or updated
        IF EXISTS (
            SELECT 1
            FROM deleted d
            WHERE EXISTS (
                -- Check if the deleted/updated product is associated with unpaid orders
                SELECT 1
                FROM [dbo].[OrderPosition] op
                JOIN [dbo].[Order] o ON op.OrderID = o.ID
                WHERE op.ProductID = d.ID AND NOT EXISTS (
                    SELECT 1 FROM [dbo].[Order] WHERE ID = op.OrderID AND IsPaid = 1
                )
            ) OR EXISTS (
                -- Check if the deleted/updated product is associated with basket positions
                SELECT 1
                FROM [dbo].[BasketPosition] bp
                WHERE bp.ProductID = d.ID
            )
        )
        BEGIN
            -- Rollback the operation and display an error message
            RAISERROR ('Cannot delete or deactivate products associated with unpaid orders or basket items.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        ELSE
        BEGIN
            -- Proceed with the operation
            IF EXISTS (SELECT * FROM inserted)
            BEGIN
                -- Update IsActive column if product is being updated
                UPDATE p
                SET p.IsActive = i.IsActive
                FROM [dbo].[Products] p
                INNER JOIN inserted i ON p.ID = i.ID;
            END
        END
    END
END;