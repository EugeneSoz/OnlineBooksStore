using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Database;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Persistence.EF;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Domain.Services
{
    public class DbDataService : IDbDataService
    {
        public DbMessageResponse SeedDatabase(DbContext context)
        {
            var message = new DbMessageResponse
            {
                MessageType = MessageType.Info,
                Message = "Необходимо применить миграции база данных перед её заполнением"
            };

            if (!context.Database.GetPendingMigrations().Any())
            {
                try
                {
                    return Seed(context);
                }
                catch (Exception ex)
                {
                    message.MessageType = MessageType.Error;
                    message.Message = ex.Message;
                }
            }

            return message;
        }

        private DbMessageResponse Seed(DbContext context)
        {
            if (context is StoreDbContext dataContext && !dataContext.Books.Any())
            {
                SeedWithTestData(dataContext);
            }
            context.SaveChanges();

            return new DbMessageResponse
            {
                MessageType = MessageType.Info,
                Message = "Данные загружены"
            };
        }

        private void SeedWithTestData(StoreDbContext context)
        {
            var publishers = new List<PublisherEntity>();
            for (int i = 0; i < 100; i++)
            {
                publishers.Add(new PublisherEntity()
                {
                    Name = $"Publisher_{i + 1}",
                    Country = "Russia",
                    Created = DateTime.Now
                });
            }

            var categories = new List<CategoryEntity>();
            for (int i = 0; i < 10; i++)
            {
                categories.Add(new CategoryEntity()
                {
                    Name = $"Category_{i + 1}",
                    Created = DateTime.Now
                });
            }

            var subcategories = new List<CategoryEntity>();
            for (int i = 0; i < 100; i++)
            {
                int categoryIndex = i / 10;
                subcategories.Add(new CategoryEntity()
                {
                    ParentCategory = categories[categoryIndex],
                    Name = $"Subcategory_{i + 1}",
                    Created = DateTime.Now
                });
            }

            var randomPrice = new Random();
            var books = new List<BookEntity>();
            for (int i = 0; i < 500; i++)
            {
                int subcategoryIndex = i / 5;
                int publisherIndex = i / 5;
                var retailPrice = randomPrice.Next(790, 3800);
                books.Add(new BookEntity()
                {
                    Title = $"Computer_book_{i + 1}",
                    Authors = $"Authors_{i + 1}",
                    Language = "Russian",
                    PageCount = 800 + i,
                    Description = $"Book description",
                    Year = 2018,
                    RetailPrice = retailPrice,
                    PurchasePrice = retailPrice - 100,
                    Publisher = publishers[publisherIndex],
                    Category = subcategories[subcategoryIndex],
                    Created = DateTime.Now
                });
            }

            context.Books.AddRange(books);
        }
    }
}
