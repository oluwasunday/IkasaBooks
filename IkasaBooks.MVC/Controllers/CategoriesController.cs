using IkasaBooks.Core;
using IkasaBooks.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepositoryMVC _categoryRepositoryMVC;
        public CategoriesController(ICategoryRepositoryMVC categoryRepositoryMVC)
        {
            _categoryRepositoryMVC = categoryRepositoryMVC;
        }



        public async Task<IActionResult> Index()
        {
            var bookCategory = await _categoryRepositoryMVC.GetCategoryNames();



            if (bookCategory == null)
            {
                ModelState.AddModelError("", "No category found");
                ViewBag.Message = $"No category found!";
                bookCategory = new List<CategoryNamesDTO>();
            }



            return View(bookCategory);
        }
    }
}
