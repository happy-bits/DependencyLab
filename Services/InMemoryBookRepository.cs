using DependencyLab.Models;

namespace DependencyLab.Services;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private readonly object _lock = new();

    public IEnumerable<Book> GetAllBooks()
    {
        lock (_lock)
        {
            return _books.ToList();
        }
    }

    public Book? GetBookById(int id)
    {
        lock (_lock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return book == null ? null : new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                IsAvailable = book.IsAvailable
            };
        }
    }

    public void AddBook(Book book)
    {
        lock (_lock)
        {
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
        }
    }

    public void UpdateBook(Book book)
    {
        lock (_lock)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                var index = _books.IndexOf(existingBook);
                _books[index] = book;
            }
        }
    }

    public void DeleteBook(int id)
    {
        lock (_lock)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}