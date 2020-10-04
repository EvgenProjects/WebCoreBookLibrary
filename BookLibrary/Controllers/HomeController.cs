using System.Linq;
using AutoMapper;
using BookLibrary.Contexts;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Models;
using BookLibrary.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
	public class HomeController : Controller
	{
        public HomeController()
		{
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ListBook", "Book");
            }

            return View();
        }
	}
}
