using BookStore.Api.Models;
using BookStore.Api.Services;
using Xunit;

namespace BookStore.Tests;

public class BookDiscountServiceTests
{
    private readonly BookDiscountService _service = new();

    [Fact]
    public void CalculateDiscountedPrice_AppliesCorrectDiscount()
    {
        var book = new Book { Id = 1, Title = "Domain-Driven Design", Price = 50.00m };

        var result = _service.CalculateDiscountedPrice(book, 20m);

        Assert.Equal(40.00m, result);
    }

    [Fact]
    public void CalculateDiscountedPrice_ThrowsOnInvalidPercentage()
    {
        var book = new Book { Id = 1, Title = "Domain-Driven Design", Price = 50.00m };

        Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalculateDiscountedPrice(book, 150m));
    }

    [Fact]
    public void IsEligibleForBulkDiscount_ReturnsTrueForLargeOrder()
    {
        var book = new Book { Id = 1, Title = "Domain-Driven Design", Price = 30.00m };

        var eligible = _service.IsEligibleForBulkDiscount(book, 6);

        Assert.True(eligible);
    }
}