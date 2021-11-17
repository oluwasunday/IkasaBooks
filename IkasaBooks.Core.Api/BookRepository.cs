using AutoMapper;
using IkasaBooks.Data;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.Core.Api
{
    public class BookRepository : IBookRepository
    {
        private readonly IkasaDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public BookRepository(IkasaDbContext dbContext, UserManager<AppUser> userManager,IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task<AddBookResponseDTO> CreateBook(CreateBookDTO createBook)
        {
            if (createBook == null)
            {
                throw new ArgumentNullException("Empty data");
            }

            var bookType = createBook.IsPdf == true ? "Pdf" : "Audio";

            var book = new Book
            {
                Name = createBook.Name,
                Authors = new List<Author>
                {
                    new Author
                    {
                        Name = createBook.AuthorName,
                        Id = Guid.NewGuid().ToString()
                    }
                },
                BookUrl = createBook.BookUrl,
                Id = Guid.NewGuid().ToString(),
                Description = createBook.Description,
                ImageURL = createBook.ImageURL,
                IsFeatured = createBook.IsFeatured,
                ISBN = createBook.ISBN,
                BookCategory = new Category
                {
                    CategoryName = createBook.CategoryName,
                    Id = Guid.NewGuid().ToString(),
                    IsPdf = createBook.IsPdf,
                    BookTypes = new List<BookType>
                    {
                        new BookType
                        {
                            Id = Guid.NewGuid().ToString(),
                            TypeName = bookType
                        }
                    }
                },
                Publisher = new Publisher { 
                    Id = Guid.NewGuid().ToString(), 
                    PublisherName = createBook.Publisher, 
                    YearPublished = createBook.YearPublished
                }
            };

            var result = await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            if (result != null)
            {
                return new AddBookResponseDTO { BookName = book.Name, Author = createBook.AuthorName, BookCategory = createBook.CategoryName };
            }
            throw new ArgumentNullException(nameof(book));
        }



        public IEnumerable<Book> GetAllBooks()
        {
            var books = _dbContext.Books.ToList();
            if (books != null)
            {
                return books;
            }
            throw new ArgumentNullException("Null data");
        }



        public async Task<bool> DeleteBook(string bookId)
        {
            if (bookId == null)
                throw new ArgumentNullException("Book id is required");

            var getBook = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);
            if (getBook != null)
            {
                _dbContext.Remove(getBook);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            throw new ArgumentNullException("Book not found!");
        }



        public Book GetBook(string bookId)
        {
            if (bookId == null)
                throw new ArgumentNullException("Book id is required");

            var getBook = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);

            if (getBook != null)
            {
                return getBook;
            }
            throw new ArgumentNullException("Book not found!");
        }


        public Book GetBookByISBN(string isbn)
        {
            if (isbn == null)
                throw new ArgumentNullException("Book id is required");

            var getBook = _dbContext.Books.FirstOrDefault(x => x.ISBN == isbn);

            if (getBook != null)
            {
                return getBook;
            }
            throw new ArgumentNullException("Book not found!");
        }


        public Book GetBookByBookName(string bookName)
        {
            if (bookName == null)
                throw new ArgumentNullException("Book id is required");

            var getBook = _dbContext.Books.FirstOrDefault(x => x.Name == bookName);

            if (getBook != null)
            {
                return getBook;
            }
            throw new ArgumentNullException("Book not found!");
        }



        public async Task<bool> UpdateBook(string bookId, UpdateBookDTO updateBook)
        {
            var getBook = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);
            if (updateBook != null)
            {
                getBook.Name = (string.IsNullOrWhiteSpace(updateBook.Name) || updateBook.Name == "string") ?
                    getBook.Name : updateBook.Name;

                var result = _dbContext.Update(getBook);
                await _dbContext.SaveChangesAsync();
                if (result != null)
                    return true;

                return false;
            }
            throw new ArgumentNullException("User not found!");
        }


        public Task<Book> UpdateBooks(string bookId)
        {
            throw new NotImplementedException();
        }


        public async Task ReviewBook(ReviewBookDTO reviewBook)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == reviewBook.UserId);
            if (user == null)
                throw new ArgumentNullException("User not found!");

            var book = _dbContext.Books.FirstOrDefault(x => x.Id == reviewBook.BookId);
            if (book == null)
                throw new ArgumentNullException("Book not found");

            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                Comments = reviewBook.Comments,
                UserId = reviewBook.UserId,
                BookId = reviewBook.BookId,
                User = user,
                Book = book
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
        }


        public async Task RateBook(RatingDTO rateBook)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == rateBook.UserId);
            if (user == null)
                throw new ArgumentNullException("User not found!");

            var book = _dbContext.Books.FirstOrDefault(x => x.Id == rateBook.BookId);
            if (book == null)
                throw new ArgumentNullException("Book not found");

            var rate = new Rating
            {
                Id = Guid.NewGuid().ToString(),
                RatingLevel = rateBook.RateLevel,
                BookId = rateBook.BookId,
                UserId = rateBook.UserId,
                Book = book,
                User = user,
            };

            _dbContext.Ratings.Add(rate);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<BookResponseDTO> SearchBookByTerm(string searchTerm)
        {
            var books = _dbContext.Books
            .Include(x => x.Authors)
           
            .Where(x => x.ISBN.Contains(searchTerm))
            .Include(y => y.Publisher)
            .Where(y => y.Publisher.PublisherName.Contains(searchTerm))
            .Where(y => y.Publisher.YearPublished.Equals(Convert.ToInt32(searchTerm)))
            .ToList();



            if (books != null)
                return _mapper.Map<IEnumerable<BookResponseDTO>>(books);



            throw new KeyNotFoundException("No item found!");
        }



        public IEnumerable<CategoryNamesDTO> GetCategorynames()
        {
            var categoryNames = new List<CategoryNamesDTO>();
            foreach (var item in _dbContext.Categories)
            {
                categoryNames.Add(new CategoryNamesDTO { CategoryNames = item.CategoryName });
            }



            if (categoryNames == null)
                throw new ArgumentNullException("No category found!");
            return categoryNames;
        }


        public async Task<IEnumerable<BookResponseDTO>> GetBooksByCategory(string categoryName)
        {
            if (categoryName == null)
                throw new ArgumentNullException("Invalid category name");

            var books = await _dbContext.Books
                .Where(x => x.BookCategory.CategoryName == categoryName)
                .Include(x => x.BookCategory)
                .Include(x => x.Publisher)
                .Include(x => x.Authors)
                .ToListAsync();

            var response = new List<BookResponseDTO>();
            foreach (var book in books)
            {
                var addBook = new BookResponseDTO
                {
                    Id = book.Id,
                    BookName = book.Name,
                    BookImageUrl = book.ImageURL,
                    BookCategory = book.BookCategory.CategoryName,
                    Publisher = book.Publisher.PublisherName,
                    YearPublished = book.Publisher.YearPublished.ToString()
                };
                response.Add(addBook);
            }

            return response;
        }
    }
}
