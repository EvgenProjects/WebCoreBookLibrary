using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Contexts
{
	public class MyDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<BookType> BookTypes { get; set; }

		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}