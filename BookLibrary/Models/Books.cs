using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore; // подключаем Entity Framework Core
using System.ComponentModel.DataAnnotations; // подключаем распознование [...]

namespace BookLibrary.Models
{
	public class BookContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<BookType> BookTypes { get; set; }

		public BookContext(DbContextOptions<BookContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}

	public class Book
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, напишите название книги")]
		[Display(Name = "Название книги")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Пожалуйста, напишите автора книги")]
		[Display(Name = "Автор")]
		public string Author { get; set; }

		[Required(ErrorMessage = "Пожалуйста, выберите Жанр книги")]
		[Display(Name = "Жанр")]
		public BookTypeInfo BookTypeId { get; set; }
		public BookType BookType { get; set; }
	}

	public class BookType
	{
		public BookTypeInfo Id { get; set; }
		public string Name { get; set; }
	}

	public enum BookTypeInfo
	{
		Fantastic = 1,
		Computer = 2,
		Classic = 3,
	}
}