using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Publisher;

namespace OnlineBooksStore.App.WebApi.Models.Database
{
    //класс для первоночального наполнения бд
    public static class SeedData
    {
        public static string SeedDatabase(DbContext context, bool fromFile)
        {
            string message = "Необходимо применить миграции база данных перед её заполнением";
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                try
                {
                    Seed(context, fromFile, ref message);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            return message;
        }

        public static string ClearDatabase(DbContext context)
        {
            string message = string.Empty;
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                try
                {
                    Clear(context, ref message);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

            }
            return message;
        }

        private static void Seed(DbContext context, bool fromFile, ref string message)
        {
            if (context is StoreDbContext dataContext && dataContext.Books.Count() == 0)
            {
                if (fromFile)
                {
                    DataRW dataRW = new DataRW();
                    dataRW.SeedDataFromFile(dataContext);
                }
                else
                {
                    SeedWithTestData(dataContext);
                }

                message = "Данные загружены";
            }
            context.SaveChanges();
        }

        private static void Clear(DbContext context, ref string message)
        {
            if (context is StoreDbContext dataContext && dataContext.Books.Count() > 0)
            {
                dataContext.Books.RemoveRange(dataContext.Books);
                dataContext.Categories.RemoveRange(dataContext.Categories);
                dataContext.Publishers.RemoveRange(dataContext.Publishers);
            }
            else
            {
                message = "Удаление не произведено, так как данные отсутствуют";
            }
            context.SaveChanges();

            message = "Данные удалены из базы данных";
            context.SaveChanges();
        }

        private static void SeedWithTestData(StoreDbContext context)
        {
            List<Publisher> publishers = new List<Publisher>();
            for (int i = 0; i < 100; i++)
            {
                publishers.Add(new Publisher
                {
                    Name = $"Publisher_{i + 1}",
                    Country = "Russia"
                });
            }

            List<Category> categories = new List<Category>();
            for (int i = 0; i < 10; i++)
            {
                categories.Add(new Category
                {
                    Name = $"Category_{i + 1}"
                });
            }

            List<Category> subcategories = new List<Category>();
            for (int i = 0; i < 100; i++)
            {
                int categoryIndex = i / 10;
                subcategories.Add(new Category
                {
                    ParentCategory = categories[categoryIndex],
                    Name = $"Subcategory_{i + 1}"
                });
            }

            Random randomPrice = new Random();
            List<Book> books = new List<Book>();
            for (int i = 0; i < 500; i++)
            {
                int subcategoryIndex = i / 5;
                int publisherIndex = i / 5;
                books.Add(new Book
                {
                    Title = $"Computer_book_{i + 1}",
                    Authors = $"Authors_{i + 1}",
                    Language = "Russian",
                    PageCount = 800 + i,
                    Description = $"Book description",
                    Year = 2018,
                    Price = randomPrice.Next(790, 3800),
                    Publisher = publishers[publisherIndex],
                    Category = subcategories[subcategoryIndex]
                });
            }

            context.Books.AddRange(books);
        }
    }
}
