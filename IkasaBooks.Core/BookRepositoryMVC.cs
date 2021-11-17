using IkasaBooks.Core.Api.interfaces;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core
{
    public class BookRepositoryMVC : IBookRepositoryMVC
    {
        public static bool userAuthenticated;
        public static string Role = "";




        public async Task<bool> CreateBook(CreateBookDTO createBook)
        {
            var bookJson = JsonConvert.SerializeObject(createBook);
            var bookPayload = new StringContent(bookJson, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.PostAsync("books/add-book", bookPayload);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            return false;
        }


        public async Task<bool> DeleteBook(string bookId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.DeleteAsync($"books/id/{bookId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = new List<Book>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync("books");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<Book>>(result);
                    //books = JsonConvert.DeserializeObject<List<Book>>(result);
                }
            }
            return books;
        }



        public async Task<Book> GetBook(string bookId)
        {
            var book = new Book();



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync($"books/{bookId}");



                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    book = JsonConvert.DeserializeObject<Book>(result);
                }
            }
            return book;
        }



        public async Task<List<BookByPublisherResponse>> GetBookByBookAuthor(string authorName)
        {
            var books = new List<BookByPublisherResponse>();



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync($"books/{authorName}");



                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<BookByPublisherResponse>>(result);
                }
            }
            return books;
        }



        public async Task<Book> GetBookByBookName(string bookName)
        {
            var book = new Book();



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync($"books/{bookName}");



                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;



                    book = JsonConvert.DeserializeObject<Book>(result);
                }
            }
            return book;
        }



        public Task<IEnumerable<BookByPublisherResponse>> GetBookByBookPublisher(string publisher)
        {
            throw new NotImplementedException();
        }



        public async Task<Book> GetBookByISBN(string isbn)
        {
            var book = new Book();



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync($"books/{isbn}");



                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    book = JsonConvert.DeserializeObject<Book>(result);
                }
            }
            return book;
        }


        public async Task<IEnumerable<BookResponseDTO>> GetBookByCategory(string categoryName)
        {
            var books = new List<BookResponseDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync($"books/category/{categoryName}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var split1 = result.IndexOf("[");
                    var split2 = result.IndexOf("]") - split1 + 1;

                    var sub = result.Substring(split1, split2);

                    books = JsonConvert.DeserializeObject<List<BookResponseDTO>>(sub);
                    
                }
            }
            return books;
        }


        public async Task<IEnumerable<BookByPublisherResponse>> GetBookByYearPublished(string yearPublished)
        {
            var books = new List<BookByPublisherResponse>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync($"books/{yearPublished}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<BookByPublisherResponse>>(result);
                }
            }
            return books;
        }


        public async Task RateBook(RatingDTO rateBook)
        {
            var books = new List<BookByPublisherResponse>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                //var response = await client.PostAsync($"books/{rateBook}", rateBook);
                var response = await client.GetAsync($"books/{rateBook}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<BookByPublisherResponse>>(result);
                }
            }
            //return books;
        }


        public async Task<bool> LoginUser(AppUser user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44361/api/v1/users/login", content))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    int roleIndex = token.IndexOf("role") + 7;
                    Role = token.Substring(roleIndex, 5);

                    
                    if (response.IsSuccessStatusCode == false)
                    {
                        userAuthenticated = false;
                        return false;
                    }
                    else
                    {
                        userAuthenticated = true;
                        return true;
                    }

                }


            }
        }

        public bool AuthenticateUser()
        {
            return userAuthenticated;
        }

        public bool DisableUser()
        {
            return userAuthenticated = false;
            //return userAuthenticated;
        }

        public string UserRole()
        {
            return Role;
        }


        public Task ReviewBook(ReviewBookDTO reviewBook)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookResponseDTO> SearchBookByTerm(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBook(string bookId, UpdateBookDTO updateBook)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> Role(AppUser user)
        //{
        //    return await authenticationRepository.Role(user);
        //}
    }
}
