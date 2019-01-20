using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
	namespace BookLibrary.Controllers
	{
		public class HomeController : Controller
		{
			BookContext db;
			public HomeController(BookContext context)
			{
				db = context;
			}
			public IActionResult Index()
			{
				var books = db.Books.Include(t => t.BookType);
				return View(books);
			}

			public ActionResult AddBook()
			{
				// get types
				ViewBag.BookTypes = from item in db.BookTypes select new SelectListItem { Text = item.Name, Value = item.Id.ToString() };

				return View();
			}

			[HttpPost]
			public ActionResult AddBook(Book book)
			{
				if (ModelState.IsValid)
				{
					db.Books.Add(book);
					db.SaveChanges();
					return RedirectToAction("Index", "Home");
				}

				// get types
				ViewBag.BookTypes = from item in db.BookTypes select new SelectListItem { Text = item.Name, Value = item.Id.ToString() };
				return View();
			}

			public ActionResult DeleteBook(Book item)
			{
				// delete
				Book book = db.Books.Find(item.Id);
				if (book != null)
				{
					db.Books.Remove(book);
					db.SaveChanges();
				}
				return RedirectToAction("Index", "Home");
			}

			[HttpGet]
			public ActionResult EditBook(int id)
			{
				// get types
				ViewBag.BookTypes = from item in db.BookTypes select new SelectListItem { Text = item.Name, Value = item.Id.ToString() };

				Book book = db.Books.FirstOrDefault(item => item.Id== id);
				return View("EditBook", book);
			}

			[HttpPost]
			public ActionResult EditBook(Book newBook)
			{
				if (ModelState.IsValid)
				{
					Book book = db.Books.Find(newBook.Id);
					if (book != null)
					{
						db.Entry(book).CurrentValues.SetValues(newBook);
						db.SaveChanges();
					}
					return RedirectToAction("Index", "Home");
				}

				// get types
				ViewBag.BookTypes = from item in db.BookTypes select new SelectListItem { Text = item.Name, Value = item.Id.ToString() };
				return View();
			}
		}
	}
}
