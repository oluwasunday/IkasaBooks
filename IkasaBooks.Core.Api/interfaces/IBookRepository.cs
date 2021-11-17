using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api
{
    public interface IBookRepository
    {
        Task<AddBookResponseDTO> CreateBook(CreateBookDTO createBook);
        Task<bool> DeleteBook(string bookId);
        IEnumerable<Book> GetAllBooks();
        Book GetBook(string bookId);
        Book GetBookByBookName(string bookName);
        Book GetBookByISBN(string isbn);
        Task<bool> UpdateBook(string bookId, UpdateBookDTO updateBook);
        Task<Book> UpdateBooks(string bookId);
        Task ReviewBook(ReviewBookDTO reviewBook);
        Task RateBook(RatingDTO rateBook);
        IEnumerable<CategoryNamesDTO> GetCategorynames();
        IEnumerable<BookResponseDTO> SearchBookByTerm(string searchTerm);
        Task<IEnumerable<BookResponseDTO>> GetBooksByCategory(string categoryName);
    }
}