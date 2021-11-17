using IkasaBooks.Core;
using IkasaBooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{
    public class LoginController : Controller
    {
       
        public static string Message = "";

        private readonly IBookRepositoryMVC bookRepo;

        public LoginController(IBookRepositoryMVC bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        //public async Task<IActionResult> LoginUser(AppUser user)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        //        using (var response = await httpClient.PostAsync("https://localhost:44361/api/v1/users/login", content))
        //        {
        //            string token = await response.Content.ReadAsStringAsync();
        //            if (response.IsSuccessStatusCode == false)
        //            {
        //                Message = "Invalid username or password";
        //                return Redirect("~/Login/Index");
        //            }
        //            HttpContext.Session.SetString("JWToken", token);
        //            //var registeredMember = response.ReasonPhrase;

        //        }

        //        return Redirect("~/Home/Index");
        //    }
        //}

        public async Task<IActionResult> LoginUser(AppUser user)
        {
            var userExists = await bookRepo.LoginUser(user);
            
            
            if (userExists)
            {
                return Redirect("~/Home/Index");
            }
            else
            {
                Message = "Invalid username or password";
                return Redirect("~/Login/Index");
            }

        }


        


        public IActionResult Index()
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
