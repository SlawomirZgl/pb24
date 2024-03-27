CREATE PROCEDURE GenerateOrder
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OrderId INT;
    INSERT INTO [Orders] (UserID, Date)
    VALUES (@UserId, GETDATE());

    SET @OrderId = SCOPE_IDENTITY();

    INSERT INTO OrderPositions (OrderID, Amount, Price)
    SELECT @OrderId, bp.Amount, p.Price
    FROM BasketPositions bp
    INNER JOIN Products p ON bp.ProductID = p.ID
    WHERE bp.UserID = @UserId;

    DELETE FROM BasketPositions
    WHERE UserID = @UserId;

    SELECT @OrderId AS OrderId;
END
