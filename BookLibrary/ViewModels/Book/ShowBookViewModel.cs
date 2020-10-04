using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookLibrary.ViewModels.Book
{
	public class ShowBookViewModel
    {
        public int Id { get; set; }

		public string Name { get; set; }

		public string Author { get; set; }

        public string BookTypeName { get; set; }
	}

}