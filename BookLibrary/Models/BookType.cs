using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore; // подключаем Entity Framework Core

namespace BookLibrary.Models
{
	public class BookType
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

}