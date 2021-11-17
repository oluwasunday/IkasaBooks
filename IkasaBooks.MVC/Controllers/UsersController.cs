using IkasaBooks.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IkasaBooks.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepositoryMVC _userRepositoryMVC;
        public UsersController(IUserRepositoryMVC userRepositoryMVC)
        {
            _userRepositoryMVC = userRepositoryMVC;
        }



        public async Task<IActionResult> Index()
        {
            var users = await _userRepositoryMVC.GetAllUsers();
            if (users == null)
                ViewBag.Message = "Error occur while retrieving users";

            return View(users);
        }
    }
}
