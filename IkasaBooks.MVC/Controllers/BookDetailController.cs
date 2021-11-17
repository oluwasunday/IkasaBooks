using IkasaBooks.Core;
using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using IkasaBooks.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{
    public class BookDetailController : Controller
    {
        private readonly IBookRepositoryMVC _bookRepositoryMVC;
        
        public BookDetailController(IBookRepositoryMVC bookRepositoryMVC)
        {
            _bookRepositoryMVC = bookRepositoryMVC;
        }
        public async Task<IActionResult> Index(string id)
        {
            var book = await _bookRepositoryMVC.GetBook(id);

            

            var status = _bookRepositoryMVC.AuthenticateUser();
            if (status == true)
            {
                ViewBag.Download = true;
            }

            else
            {
                ViewBag.Download = false;
            }

            //var reviewBookDto = new ReviewBookDTO { }
            return View(book);
            //return View();
        }


        public IActionResult BookById(string id)
        {
            var book = _bookRepositoryMVC.GetBook(id);
            return View(book);
        }



        public IActionResult DownLoad()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DownLoad(Book model)
        {
            //here i put the PdfFiles folder in the wwwroot folder
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", "books/pdf/" + model.Name + ".pdf");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", Path.GetFileName(path));
        }
    }
}
