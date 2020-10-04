using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using BookLibrary.Contexts;
using BookLibrary.Models;
using BookLibrary.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookLibrary.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly MyDbContext _myDbContext;
        private readonly IMapper _mapper;

        public AuthenticationController(MyDbContext myDbContext, IMapper mapper)
		{
			_myDbContext = myDbContext;
            _mapper = mapper;
		}

        public ActionResult Login()
		{
			return View();
		}

		public void FillPrincipalIdentity(string userName, string email)
		{
            // create Identity
            ClaimsIdentity identity = new ClaimsIdentity("My");
            
            // add Claims
			identity.AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, email)
            });

            // create Principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // login
			HttpContext.SignInAsync(principal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = (DateTime.Now.AddHours(8)) });

            // set identity
            HttpContext.User = new ClaimsPrincipal(identity);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(UserLoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = _myDbContext.Users.FirstOrDefault(u => u.LoginEmail == model.LoginEmail && u.Password == model.Password);

				if (user != null)
				{
					FillPrincipalIdentity(user.UserName, user.LoginEmail);

					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", "Error! User not exist with this email and password");
			}

			return View(model);
		}

		public ActionResult Logoff()
		{
			HttpContext.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(UserRegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = _myDbContext.Users.FirstOrDefault(u => u.LoginEmail == model.LoginEmail);

				if (user == null)
				{
                    user = _mapper.Map<User>(model);

					_myDbContext.Users.Add(user);
					_myDbContext.SaveChanges();

					FillPrincipalIdentity(model.UserName, model.LoginEmail);

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Error! User with this Email already exists");
				}
			}

			return View(model);
		}
	}
}