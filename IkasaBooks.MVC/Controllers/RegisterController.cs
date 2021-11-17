using IkasaBooks.Core;
using IkasaBooks.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IkasaBooks.MVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepositoryMVC _userRepositoryMVC;
        public RegisterController(IUserRepositoryMVC userRepositoryMVC)
        {
            _userRepositoryMVC = userRepositoryMVC;
        }
        public IActionResult Index()
        { 
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(AddUserDTO userDTO)
        {
            userDTO.Username = userDTO.Email;
            _userRepositoryMVC.AddNewUser(userDTO);

            ViewBag.Message = "User successfully created";
            return Redirect("Index");
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
