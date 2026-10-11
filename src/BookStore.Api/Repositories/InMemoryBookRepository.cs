using BookStore.Api.Interfaces;
using BookStore.Api.Models;

namespace BookStore.Api.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books =
    [
        new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Price = 35.00m, StockQuantity = 12 },
        new Book { Id = 2, Title = "Design Patterns", Author = "Gang of Four", Price = 45.00m, StockQuantity = 8 },
        new Book { Id = 3, Title = "Refactoring", Author = "Martin Fowler", Price = 40.00m, StockQuantity = 5 }
    ];

    public Task<IEnumerable<Book>> GetAllAsync() => Task.FromResult<IEnumerable<Book>>(_books);

    public Task<Book?> GetByIdAsync(int id) =>
        Task.FromResult(_books.FirstOrDefault(b => b.Id == id));

    public Task<Book> AddAsync(Book book)
    {
        book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
        _books.Add(book);
        return Task.FromResult(book);
    }

    public Task<bool> UpdateAsync(Book book)
    {
        var existing = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existing == null) return Task.FromResult(false);
        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Price = book.Price;
        existing.StockQuantity = book.StockQuantity;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var existing = _books.FirstOrDefault(b => b.Id == id);
        if (existing == null) return Task.FromResult(false);
        _books.Remove(existing);
        return Task.FromResult(true);
    }
}