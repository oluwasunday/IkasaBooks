using IkasaBooks.Core;
using IkasaBooks.Models.DTOs;
using IkasaBooks.MVC.Models;
using IkasaBooks.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{
   public class HomeController : Controller
    {
        public static bool Logout = false;
        private readonly ILogger<HomeController> _logger;

        private readonly ICategoryRepositoryMVC _categoryRepositoryMVC;
        private readonly IBookRepositoryMVC _bookRepositoryMVC;
    
        public HomeController(ILogger<HomeController> logger, ICategoryRepositoryMVC categoryRepositoryMVC,
            IBookRepositoryMVC bookRepositoryMVC)
        {
            _categoryRepositoryMVC = categoryRepositoryMVC;
            _logger = logger;
            _bookRepositoryMVC = bookRepositoryMVC;
        }


        public  async Task<IActionResult> Index()
        {
            var bookCategory = await _categoryRepositoryMVC.GetCategoryNames();
            var homeViewModel = new HomeViewModel();

            if (bookCategory == null)
            {
                ModelState.AddModelError("", "No category found");
                ViewBag.Message = $"No category found!";
                bookCategory = new List<CategoryNamesDTO>();
            }


            var books = await _bookRepositoryMVC.GetAllBooks();
            if (books == null)
                ViewBag.Message = "Error occur while retrieving books";// return View(books);
         

            homeViewModel.Books = books;
            homeViewModel.Categories = bookCategory;
            homeViewModel.IsFeatured = books.Where(x => x.IsFeatured == true);
            var token = HttpContext.Session.GetString("JWToken");

            var status = _bookRepositoryMVC.AuthenticateUser();
            //if (status == true && Logout == false)
            if (status == true)
             {
                ViewBag.Status = "Logout";
            }

            else
            {
                ViewBag.Status = "Login";
            }

            var role = _bookRepositoryMVC.UserRole();
            if(role == "Admin")
            {
                ViewBag.Role = "Admin";
            }
                       

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logoff()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var disableUser = _bookRepositoryMVC.DisableUser();
            HttpContext.Session.Clear(); //clear token
          // Logout = true;

            return Redirect("~/Home/Index");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
