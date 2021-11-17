using IkasaBooks.Core;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using IkasaBooks.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{
    public class BooksController : Controller
    {
       
        private readonly IBookRepositoryMVC _bookRepositoryMVC;
        private readonly ICategoryRepositoryMVC _categoryRepositoryMVC;
        public BooksController(IBookRepositoryMVC bookRepositoryMVC, ICategoryRepositoryMVC categoryRepositoryMVC)
        {
            _bookRepositoryMVC = bookRepositoryMVC;
            _categoryRepositoryMVC = categoryRepositoryMVC;
        }

        public async Task<IActionResult> Index(string categoryname)
        {
            var homeViewModel = new HomeViewModel();
            if (categoryname == null) 
            {
                homeViewModel.Books = await _bookRepositoryMVC.GetAllBooks();
                homeViewModel.Categories = await _categoryRepositoryMVC.GetCategoryNames();

                if (homeViewModel.Books == null)
                    ViewBag.Message = "Error occur while retrieving books";
                return View(homeViewModel);
            }


            homeViewModel.BooksByCategory = await _bookRepositoryMVC.GetBookByCategory(categoryname);
            homeViewModel.Categories = await _categoryRepositoryMVC.GetCategoryNames();

            return View(homeViewModel);
        
        }
         
        public async Task<IActionResult> GetBookById(string id)
        {
            var book = await _bookRepositoryMVC.GetBook(id);
            if (book == null)
            {
                ModelState.AddModelError("", "errorMessage getting expected book");
                ViewBag.Message = $"Error occur while retrieving book with id {id}" +
                $" from the database or no book with the id exists";
                book = new Book();
            }
            return View(book);
        }

        public async Task<IActionResult> GetBookByTitle(string bookTitle)
        {
            var book = await _bookRepositoryMVC.GetBookByBookName(bookTitle);
            if (book == null)
            {
                ModelState.AddModelError("", "errorMessage getting expected book");
                ViewBag.Message = $"Error occur while retrieving book with title {bookTitle}" +
                $" from the database or no book with the id exists";
                book = new Book();
            }
            return View(book);
        }

        public async Task<IActionResult> GetBookByCategory(string categoryName)
        {
            var book = await _bookRepositoryMVC.GetBookByCategory(categoryName);
            if (book == null)
            {
                ModelState.AddModelError("", "errorMessage getting expected book");
                ViewBag.Message = $"Error occur while retrieving book with title {categoryName}" +
                $" from the database or no book with the id exists";
                book = null;
            }
            return View(book);
        }
    }
}
