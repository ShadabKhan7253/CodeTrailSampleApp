using BookStore.Api.Interfaces;
using BookStore.Api.Models;

namespace BookStore.Api.Services;

public class BookCatalogService(IBookRepository repository, BookDiscountService discountService)
{
    public async Task<IEnumerable<Book>> GetCatalogAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Book?> GetBookDetailsAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<decimal> GetPromotionalPriceAsync(int bookId, decimal discountPercent)
    {
        var book = await repository.GetByIdAsync(bookId)
            ?? throw new KeyNotFoundException($"Book with ID {bookId} not found.");

        return discountService.CalculateDiscountedPrice(book, discountPercent);
    }
}