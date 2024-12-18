using DependencyLab.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyLab.Controllers;

public class BookController(IBookService bookService) : Controller
{

    public IActionResult Index()
    {
        var books = bookService.GetAvailableBooks();
        return View(books);
    }

    public IActionResult Borrow(int id)
    {
        if (bookService.BorrowBook(id))
        {
            TempData["Message"] = "Book borrowed successfully";
        }
        else
        {
            TempData["Error"] = "Could not borrow the book";
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Return(int id)
    {
        if (bookService.ReturnBook(id))
        {
            TempData["Message"] = "Book returned successfully";
        }
        else
        {
            TempData["Error"] = "Could not return the book";
        }
        return RedirectToAction(nameof(Index));
    }
}