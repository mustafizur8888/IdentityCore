using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PluralsightDemo.Models;

namespace PluralsightDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<PluralsightUser> _userManager;

        public HomeController(UserManager<PluralsightUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(); 
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                PluralsightUser pluralsightUser = await _userManager.FindByNameAsync(model.UserName);
                if (pluralsightUser==null)
                {
                    PluralsightUser user = new PluralsightUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    return View("Success");
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                PluralsightUser pluralsightUser = await _userManager.FindByNameAsync(model.UserName);
                if (pluralsightUser==null)
                {
                    PluralsightUser user = new PluralsightUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    return View("Success");
                }
            }
            return View();
        }
    }
}
