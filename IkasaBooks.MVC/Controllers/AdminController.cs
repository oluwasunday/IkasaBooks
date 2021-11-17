using IkasaBooks.Core;
using IkasaBooks.Models.DTOs;
using IkasaBooks.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{

    public class AdminController : Controller
    {
        private readonly ICategoryRepositoryMVC _categoryRepositoryMVC;
        private readonly IBookRepositoryMVC _bookRepositoryMVC;
        private readonly IUserRepositoryMVC _userRepositoryMVC;

        public AdminController(ICategoryRepositoryMVC categoryRepositoryMVC, IBookRepositoryMVC bookRepositoryMVC,
            IUserRepositoryMVC userRepositoryMVC)
        {
            _categoryRepositoryMVC = categoryRepositoryMVC;
            _bookRepositoryMVC = bookRepositoryMVC;
            _userRepositoryMVC = userRepositoryMVC;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddBook()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(CreateBookDTO createBook)
        {
            var create = await _bookRepositoryMVC.CreateBook(createBook);

            if (create == true)
            {
                ViewBag.Message = "User successfully created";
                return Redirect("Index");
            }

            return Content("Not Successful!");
        }


        public async Task<IActionResult> AllBooks()
        {
            var homeViewModel = new HomeViewModel();

            var books = await _bookRepositoryMVC.GetAllBooks();
            if (books == null)
                ViewBag.Message = "Error occur while retrieving books";// return View(books);


            homeViewModel.Books = books;
            homeViewModel.IsFeatured = books.Where(x => x.IsFeatured == true);
            var token = HttpContext.Session.GetString("JWToken");

            return View(homeViewModel);
        }




        public async Task<IActionResult> DeleteBookById(string id)
        {
            var create = await _bookRepositoryMVC.DeleteBook(id);

            if (create == true)
            {
                ViewBag.Message = "User successfully created";
                return Redirect("Index");
            }

            return Content("Not Successful!");
        }


        public async Task<IActionResult> Users()
        {
            var homeViewModel = new HomeViewModel();

            var users = await _userRepositoryMVC.GetAllUsers();
            if (users == null)
                ViewBag.Message = "Error occur while retrieving users";


            homeViewModel.Users = users;
            var token = HttpContext.Session.GetString("JWToken");

            return View(homeViewModel);
        }
    }
}
