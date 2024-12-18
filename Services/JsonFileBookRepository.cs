using System.Text.Json;
using DependencyLab.Models;

namespace DependencyLab.Services;

public class JsonFileBookRepository(ILogger<JsonFileBookRepository> logger) : IBookRepository
{
    private readonly string _filePath = "books.json";
    private List<Book> _books = [];

    private void LoadBooks()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _books = JsonSerializer.Deserialize<List<Book>>(json) ?? [];
            logger.LogInformation("Books loaded from JSON file");
        }
    }

    private void SaveBooks()
    {
        var json = JsonSerializer.Serialize(_books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
        logger.LogInformation("Books saved to JSON file");
    }

    public IEnumerable<Book> GetAllBooks()
    {
        LoadBooks();
        return _books;
    }

    public Book? GetBookById(int id)
    {
        LoadBooks();
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public void AddBook(Book book)
    {
        LoadBooks();
        book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
        _books.Add(book);
        SaveBooks();
    }

    public void UpdateBook(Book book)
    {
        LoadBooks();
        var index = _books.FindIndex(b => b.Id == book.Id);
        if (index != -1)
        {
            _books[index] = book;
            SaveBooks();
        }
    }

    public void DeleteBook(int id)
    {
        LoadBooks();
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _books.Remove(book);
            SaveBooks();
        }
    }
} 