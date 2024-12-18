using DependencyLab.Models;

namespace DependencyLab.Services;

public interface IBookService
{
    IEnumerable<Book> GetAvailableBooks();
    bool BorrowBook(int id);
    bool ReturnBook(int id);
    void AddNewBook(Book book);
} 