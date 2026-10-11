using BookStore.Api.Models;

namespace BookStore.Api.Services;

public class BookDiscountService
{
    public decimal CalculateDiscountedPrice(Book book, decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100.");

        var discountAmount = book.Price * (discountPercentage / 100m);
        return Math.Round(book.Price - discountAmount, 2);
    }

    public bool IsEligibleForBulkDiscount(Book book, int quantity)
    {
        return quantity >= 5 && book.Price >= 20.00m;
    }

    /// <summary>
    /// Calculates discount for VIP loyalty club members (25% base discount).
    /// </summary>
    public decimal CalculateVipDiscount(Book book, decimal extraBonusPercentage = 0)
    {
        var totalDiscount = Math.Min(25.00m + extraBonusPercentage, 50.00m);
        return CalculateDiscountedPrice(book, totalDiscount);
    }
}
