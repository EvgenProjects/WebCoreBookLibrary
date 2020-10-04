using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.ViewModels.User
{
	public class UserRegisterViewModel
	{
		[Required(ErrorMessage = "Пожалуйста, введите Ваше имя")]
		[Display(Name = "UserName")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите Email")]
		[Display(Name = "Email")]
		public string LoginEmail { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите Пароль")]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите Пароль")]
		[Display(Name = "Подтвердить Пароль")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string ConfirmPassword { get; set; }
	}
}