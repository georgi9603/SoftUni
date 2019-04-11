using BookShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Initializer;

namespace BookShop
{
    using Data;
    using System.Globalization;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
              //  DbInitializer.ResetDatabase(db);

                int result = RemoveBooks(db);

                Console.WriteLine(result);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(r => r.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(x => x)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var editionType = Enum.Parse<EditionType>("Gold", true);

            //Return in a single string titles of the golden edition books that have less than 5000 copies,
            //each on a new line.Order them by book id ascending.
            var books = context.Books
                .Where(s => s.EditionType == editionType)
                .Where(c => c.Copies < 5000)
                .Select(b => b.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var books = context.Books
                .Where(book => book.Price > 40)
                .OrderByDescending(book => book.Price)
                .Select(book => new
                {
                    book.Title,
                    book.Price
                });

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return result.ToString();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder result = new StringBuilder();

            var books = context.Books
                 .Select(x => new
                 {
                     x.Title,
                     x.ReleaseDate,
                     x.BookId
                 })
                 .Where(x => x.ReleaseDate.Value.Year != year)
                 .OrderBy(x => x.BookId);

            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            List<string> categoriesList = input
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var books = context.Books
                .Where(book => book.BookCategories.Any(c => categoriesList.Contains(c.Category.Name.ToLower())))
                .Select(book => book.Title)
                .OrderBy(c => c)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder result = new StringBuilder();

            DateTime inputDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                new CultureInfo("en-CA"));

            var books = context.Books
                .Select(x => new
                {
                    x.Title,
                    x.EditionType,
                    x.Price,
                    x.ReleaseDate
                })
                .Where(x => x.ReleaseDate.Value < inputDate)
                .OrderByDescending(x => x.ReleaseDate)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return result.ToString();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(author => new
                {
                    Fullname = $"{author.FirstName} {author.LastName}"
                })
                .OrderBy(x => x.Fullname)
                .ToList();


            foreach (var author in authors)
            {
                result.AppendLine(author.Fullname);
            }

            return result.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var books = context.Books
                .Where(b => b.Title.ToLower()
                        .Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .ToList();


            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().Trim();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var books = context.Books
                .Where(b => b.Author
                            .LastName.ToLower()
                                .StartsWith(input.ToLower()))
                .Select(b => new
                {
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();


            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return result.ToString().Trim();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Count(x => x.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var authors = context.Authors
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BoooksCount = x.Books.Sum(s => s.Copies)
                })
                .OrderByDescending(a => a.BoooksCount)
                .ToList();

            foreach (var author in authors)
            {
                result.AppendLine($"{author.FirstName} {author.LastName} - {author.BoooksCount}");
            }

            return result.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    profit = x.CategoryBooks.Sum(y => y.Book.Copies * y.Book.Price)
                })
                .OrderByDescending(x => x.profit)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var categorie in categories)
            {
                result.AppendLine($"{categorie.Name} ${categorie.profit:F2}");
            }

            return result.ToString().Trim();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    books = x.CategoryBooks.Select(s => new
                    {
                        bookReleaseDate = s.Book.ReleaseDate,
                        bookName = s.Book.Title
                    })
                        .OrderByDescending(s => s.bookReleaseDate)
                        .Take(3)
                        .ToList()
                })
                .OrderBy(b => b.Name)
                .ToList();

            foreach (var categorie in categories)
            {
                result.AppendLine($"--{categorie.Name}");

                foreach (var book in categorie.books)
                {
                    result.AppendLine($"{book.bookName} ({book.bookReleaseDate.Value.Year})");
                }
            }

            return result.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            //context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksForDelete = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.RemoveRange(booksForDelete);
            context.SaveChanges();

            //return context.SaveChanges(); not working in judge

            return booksForDelete.Count;
        }
    }
}
