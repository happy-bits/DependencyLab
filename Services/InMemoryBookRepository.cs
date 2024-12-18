using DependencyLab.Models;

namespace DependencyLab.Services;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books =
    [
        new Book { Id = 1, Title = "C# in Depth", Author = "Jon Skeet", ISBN = "123-456", IsAvailable = true },
        new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", ISBN = "789-012", IsAvailable = true }
    ];

    public IEnumerable<Book> GetAllBooks() => _books;

    public Book? GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public void AddBook(Book book)
    {
        book.Id = _books.Max(b => b.Id) + 1;
        _books.Add(book);
    }

    public void UpdateBook(Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook != null)
        {
            var index = _books.IndexOf(existingBook);
            _books[index] = book;
        }
    }

    public void DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }
}