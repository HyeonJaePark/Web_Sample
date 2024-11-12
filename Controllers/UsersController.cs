using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Sample.Models;
using Web_Sample.Services;

namespace Web_Sample.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View(); 
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate the user
                var user = _userService.Authenticate(model.Username, model.Password);
                if (user != null)
                {
                    // Set the user in the session and redirect to the appropriate page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Display an error message
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If model is invalid or authentication fails, return the view
            return View(model);
        }
        
        [HttpPost]
        public IActionResult SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password // In a real application, make sure to hash the password
                };

                // Add the user using the Create method
                _userService.Create(user);

                // Redirect to the login page or a welcome page
                return RedirectToAction("Login", "Users");
            }

            // If model is invalid or authentication fails, return the view
            return View(model);
        }

        public IActionResult Logout()
        {
            // Clear the user session and redirect to the home page
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
    }
}
