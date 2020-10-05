using System.Linq;
using AutoMapper;
using BookLibrary.Contexts;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using BookLibrary.ViewModels.Book;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookLibrary.Controllers
{
	public class BookController : Controller
	{
        // MyDbContext using for load data from database
        // MyDbContext using for store data into database
        private readonly MyDbContext _myDbContext;

        // IMapper convert Models => ViewModels
        // IMapper convert ViewModels => Models
        private readonly IMapper _mapper;

        // constructor
        public BookController(MyDbContext context, IMapper mapper)
		{
            _myDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListBook()
        {
            // In method "Include" we choose what navigation property need fill(load)
            // navigation property it is c# class object corresponded to table in database
            // navigation key it is c# long value corresponded to table key in database
            // If call method "Include(a => a.BookType)" then navigation property(BookType) will be fill using navigation key(BookTypeId)
            // Note! BookTypeId we not use in method "Include".
            // Because Microsoft library automatically knows that "name for navigation key" = "navigation property" + "Id". Example: "BookTypeId" = "BookType" + "Id"
            IEnumerable<Book> books = _myDbContext.Books.Include(a => a.BookType);

            // convert IEnumerable<Book> => IEnumerable<ShowBookViewModel> 
            // for each element mapper will convert Book => ShowBookViewModel
            IEnumerable<ShowBookViewModel> booksViewModel = _mapper.Map<IEnumerable<ShowBookViewModel>>(books);

            // show view
			return View(booksViewModel);
		}

        [HttpGet]
		public ActionResult AddBook()
        {
            var model = new CreateBookViewModel()
            {
                // items for Dropdown list
                BookTypesForDropdownList = GetBookTypesForDropdownList()
            };

            return View(model);
		}

		[HttpPost]
		public ActionResult AddBook(CreateBookViewModel model)
		{
            if (!ModelState.IsValid)
            {
                // items for Dropdown list
                model.BookTypesForDropdownList = GetBookTypesForDropdownList();

                // show view
                return View(model);
            }

            // convert CreateBookViewModel => Book
            Book book = _mapper.Map<Book>(model);

            // find email for current user (login user)
            string email = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;

            // find user id by user email
            book.UserId = _myDbContext.Users.Single(u => u.LoginEmail== email).Id;

            // add book in context
            _myDbContext.Books.Add(book);

            // store context in database
            _myDbContext.SaveChanges();

			return RedirectToAction("ListBook");
		}

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            Book book = _myDbContext.Books.FirstOrDefault(item => item.Id == id);

            // convert Book => EditBookViewModel
            EditBookViewModel model = _mapper.Map<EditBookViewModel>(book);

            // items for Dropdown list
            model.BookTypesForDropdownList = GetBookTypesForDropdownList();

            return View("EditBook", model);
        }

        [HttpPost]
        public ActionResult EditBook(EditBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // items for Dropdown list
                model.BookTypesForDropdownList = GetBookTypesForDropdownList();

                // show view
                return View(model);
            }

            Book book = _myDbContext.Books.Find(model.Id);

            // map EditBookViewModel => Book
            _mapper.Map(model, book);

            // update book in context
            _myDbContext.Books.Update(book);

            // store context in database
            _myDbContext.SaveChanges();

            return RedirectToAction("ListBook");
        }

		public ActionResult DeleteBook(Book item)
		{
			// find book
			Book book = _myDbContext.Books.Find(item.Id);
            
			// delete book in context
            _myDbContext.Books.Remove(book);

            // store context in database
            _myDbContext.SaveChanges();

            return RedirectToAction("ListBook");
		}

        // fill items for DropdownList
        protected IEnumerable<SelectListItem> GetBookTypesForDropdownList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Add book type...",
                Value = "-1"
            });

            var items = _myDbContext.BookTypes.Select(item =>
                new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });

            list.AddRange(items);

            return list;
        } 

	}
}