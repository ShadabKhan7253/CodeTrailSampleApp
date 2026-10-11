using Microsoft.AspNetCore.Mvc;
using BookStore.Api.Models;
using BookStore.Api.Services;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(BookCatalogService catalogService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await catalogService.GetCatalogAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await catalogService.GetBookDetailsAsync(id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpGet("{id:int}/promotional-price")]
    public async Task<IActionResult> GetPromotionalPrice(int id, [FromQuery] decimal discountPercent = 10)
    {
        try
        {
            var price = await catalogService.GetPromotionalPriceAsync(id, discountPercent);
            return Ok(new { bookId = id, promotionalPrice = price });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}