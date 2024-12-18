using DependencyLab.Models;

namespace DependencyLab.Services;

public class EnhancedBookService(
    IBookRepository bookRepository,
    INotificationService notificationService,
    ILogger<EnhancedBookService> logger) : IBookService
{
    private readonly Dictionary<int, int> _borrowCount = new();

    public IEnumerable<Book> GetAvailableBooks()
    {
        var books = bookRepository.GetAllBooks();
        logger.LogInformation("Retrieved {Count} books in total", books.Count());
        return books.Where(b => b.IsAvailable);
    }

    public bool BorrowBook(int id)
    {
        var book = bookRepository.GetBookById(id);
        if (book == null || !book.IsAvailable)
        {
            logger.LogWarning("Failed to borrow book {Id}: Book not available", id);
            return false;
        }

        if (_borrowCount.TryGetValue(id, out int count) && count >= 10)
        {
            logger.LogWarning("Book {Title} has been borrowed too many times and needs maintenance", book.Title);
            notificationService.SendNotification("librarian", $"Book '{book.Title}' needs maintenance check");
            return false;
        }

        book.IsAvailable = false;
        bookRepository.UpdateBook(book);
        
        _borrowCount[id] = count + 1;
        
        logger.LogInformation("Book {Title} has been borrowed (Borrow count: {Count})", book.Title, _borrowCount[id]);
        notificationService.SendNotification("admin", $"Book '{book.Title}' has been borrowed (Borrow count: {_borrowCount[id]})");
        return true;
    }

    public bool ReturnBook(int id)
    {
        var book = bookRepository.GetBookById(id);
        if (book == null)
        {
            logger.LogWarning("Failed to return book {Id}: Book not found", id);
            return false;
        }

        book.IsAvailable = true;
        bookRepository.UpdateBook(book);
        
        logger.LogInformation("Book {Title} has been returned", book.Title);
        notificationService.SendNotification("admin", $"Book '{book.Title}' has been returned and is available");
        return true;
    }

    public void AddNewBook(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.ISBN))
        {
            throw new ArgumentException("ISBN is required");
        }

        if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
        {
            throw new ArgumentException("Title and Author are required");
        }

        bookRepository.AddBook(book);
        logger.LogInformation("New book added: {Title} by {Author}", book.Title, book.Author);
        notificationService.SendNotification("librarian", $"New book cataloged: {book.Title} by {book.Author}");
    }
} 