CREATE PROCEDURE ChangeAmountOfProductsInBasket
    @BasketPositionId INT,
    @Amount INT
AS
BEGIN
    SET NOCOUNT ON;

    IF @Amount > 0
    BEGIN
        UPDATE BasketPositions
        SET Amount = @Amount
        WHERE Id = @BasketPositionId;

        SELECT @@ROWCOUNT;
    END
    ELSE
    BEGIN
        SELECT 0; -- Invalid amount
    END
END
