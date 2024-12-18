using System.Globalization;
using CsvHelper;
using DependencyLab.Models;

namespace DependencyLab.Services;

public class CsvFileBookRepository(ILogger<CsvFileBookRepository> logger) : IBookRepository
{
    private readonly string _filePath = "books.csv";
    private List<Book> _books = [];

    private void LoadBooks()
    {
        if (File.Exists(_filePath))
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            _books = csv.GetRecords<Book>().ToList();
            logger.LogInformation("Books loaded from CSV file");
        }
    }

    private void SaveBooks()
    {
        using var writer = new StreamWriter(_filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(_books);
        logger.LogInformation("Books saved to CSV file");
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