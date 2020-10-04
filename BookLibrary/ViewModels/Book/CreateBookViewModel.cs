using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookLibrary.ViewModels.Book
{
	public class CreateBookViewModel
    {
        [Required(ErrorMessage = "Please, write Book name")]
        [Display(Name = "Book name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, write Author")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Type")]
        public int? BookTypeId { get; set; }

        public IEnumerable<SelectListItem> BookTypesForDropdownList { get; set; }
	}

}