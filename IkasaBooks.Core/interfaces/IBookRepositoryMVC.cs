using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IkasaBooks.Core
{
    public interface IBookRepositoryMVC
    {
        Task<bool> CreateBook(CreateBookDTO createBook);
        Task<bool> DeleteBook(string bookId);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBook(string bookId);
        Task<List<BookByPublisherResponse>> GetBookByBookAuthor(string authorName);
        Task<Book> GetBookByBookName(string bookName);
        Task<IEnumerable<BookByPublisherResponse>> GetBookByBookPublisher(string publisher);
        Task<Book> GetBookByISBN(string isbn);
        Task<IEnumerable<BookByPublisherResponse>> GetBookByYearPublished(string yearPublished);
        Task RateBook(RatingDTO rateBook);
        Task ReviewBook(ReviewBookDTO reviewBook);
        IEnumerable<BookResponseDTO> SearchBookByTerm(string searchTerm);
        Task<bool> UpdateBook(string bookId, UpdateBookDTO updateBook);
        Task<IEnumerable<BookResponseDTO>> GetBookByCategory(string categoryName);
        Task<bool> LoginUser(AppUser user);
        bool AuthenticateUser();
        bool DisableUser();
        string UserRole();



    }
}