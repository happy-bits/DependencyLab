using DependencyLab.Models;

namespace DependencyLab.Services;

public class BookService(
    IBookRepository bookRepository,
    INotificationService notificationService,
    ILogger<BookService> logger) : IBookService
{
    public IEnumerable<Book> GetAvailableBooks()
    {
        return bookRepository.GetAllBooks().Where(b => b.IsAvailable);
    }

    public bool BorrowBook(int id)
    {
        var book = bookRepository.GetBookById(id);
        if (book == null || !book.IsAvailable)
        {
            return false;
        }

        book.IsAvailable = false;
        bookRepository.UpdateBook(book);
        logger.LogInformation("Book {Title} has been borrowed", book.Title);
        notificationService.SendNotification("admin", $"Book '{book.Title}' has been borrowed");
        return true;
    }

    public bool ReturnBook(int id)
    {
        var book = bookRepository.GetBookById(id);
        if (book == null)
        {
            return false;
        }

        book.IsAvailable = true;
        bookRepository.UpdateBook(book);
        logger.LogInformation("Book {Title} has been returned", book.Title);
        notificationService.SendNotification("admin", $"Book '{book.Title}' has been returned");
        return true;
    }

    public void AddNewBook(Book book)
    {
        bookRepository.AddBook(book);
        logger.LogInformation("New book added: {Title}", book.Title);
        notificationService.SendNotification("admin", $"New book added: {book.Title}");
    }
}