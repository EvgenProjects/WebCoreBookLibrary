using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
	public static class BooksInit
	{
		public static void Initialize(BookContext context)
		{
			if (!context.Books.Any())
			{
				// добавляем книги
				context.BookTypes.AddRange(
					new BookType
					{
						Id = BookTypeInfo.Fantastic,
						Name = "Фантастика"
					},

					new BookType
					{
						Id = BookTypeInfo.Computer,
						Name = "Программирование"
					},

					new BookType
					{
						Id = BookTypeInfo.Classic,
						Name = "Классика"
					}
				);

				// добавляем книги
				context.Books.AddRange(
					new Book
					{
						Name = "Фантастическое путешествие",
						Author = "Айзек Азимов",
						BookTypeId = BookTypeInfo.Fantastic,
					},

					new Book
					{
						Name = "Неукротимая планета",
						Author = "Гарри Гаррисон",
						BookTypeId = BookTypeInfo.Fantastic,
					},

					new Book
					{
						Name = "Изучаем C#",
						Author = "Эндрю Стиллмен",
						BookTypeId = BookTypeInfo.Computer,
					},
					
					new Book
					{
						Name = "Мастер и Маргарита",
						Author = "Михаил Булгаков",
						BookTypeId = BookTypeInfo.Classic,
					}
				);

				// сохраняем в базу данных
				context.SaveChanges();
			}
		}
	}
}