using BookStore.Api.Interfaces;
using BookStore.Api.Models;
using BookStore.Api.Repositories;
using BookStore.Api.Services;
using Xunit;

namespace BookStore.Tests;

public class BookCatalogServiceTests
{
    private readonly InMemoryBookRepository _repository = new();
    private readonly BookDiscountService _discountService = new();

    [Fact]
    public async Task GetPromotionalPriceAsync_CalculatesCorrectDiscount()
    {
        var service = new BookCatalogService(_repository, _discountService);

        // Book #1 in InMemoryBookRepository is .00
        var promoPrice = await service.GetPromotionalPriceAsync(1, 10m);

        Assert.Equal(31.50m, promoPrice);
    }
}