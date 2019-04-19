using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using BeltExam.Models;



namespace LoginController.Controllers
{
    public class LoginController : Controller
    {
        public BeltContext dbContext;

        public LoginController(BeltContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {

            return View("Index");
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        { 
                  
            if(ModelState.IsValid)
            {
                // If a User exists with provided email
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Success","Home");
                }
            }
            else
            {
                return View("Index");
            }
           
           
        }

        [Route("login")]
        [HttpGet]
        public IActionResult LoginPage()
        {
            return View("Login");
        }

        [HttpPost("loginuser")]
        public IActionResult Login(LoginUser userSubmit)
        {
            System.Console.WriteLine("User Submit");
            System.Console.WriteLine(userSubmit);
            System.Console.WriteLine(userSubmit.LoginEmail);
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(us =>us.Email == userSubmit.LoginEmail);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();

               
                var result = hasher.VerifyHashedPassword(userSubmit, userInDb.Password, userSubmit.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Password");
                    return View("Login");
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Success","Home");
                }
            } 
            return View("Login");
        }

       
        [Route("/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
           HttpContext.Session.Clear();
            return View("Login");
        }


    }
}
