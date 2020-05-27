using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Database;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Route("api/options")]
    [ApiController]
    [Produces("application/json")]
    public class DataOptionsController : ControllerBase
    {
        private readonly MigrationsManager _manager;
        private readonly IBooksRepository _bookRepo;
        private readonly ICategoriesRepository _categoryRepo;
        private readonly IPublishersRepository _publisherRepo;

        public DataOptionsController(MigrationsManager manager,
            IBooksRepository bookRepo,
            ICategoriesRepository categoryRepo,
            IPublishersRepository publisherRepo)
        {
            _manager = manager;
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
            _publisherRepo = publisherRepo;
        }

        [HttpGet("services")]
        public MigrationsOptions DbServices()
        {
            MigrationsOptions migrationsOptions = GetMigrationsOptions();

            return migrationsOptions;
        }

        [HttpGet("context/{contextName}")]
        public MigrationsOptions ChooseContext(string contextName)
        {
            return GetMigrationsOptions(contextName);
        }

        [HttpGet("apply/{contextName}/{migrationName}")]
        public MigrationsOptions ApplyMigrationsAsync(string contextName, string migrationName)
        {
            string infoMessage = string.Empty;
            try
            {
                _manager.Migrate(contextName, migrationName);
                infoMessage = "Миграции применены";
            }
            catch (Exception ex)
            {
                infoMessage = ex.Message;
            }

            return GetMigrationsOptions(contextName, infoMessage);
        }

        private MigrationsOptions GetMigrationsOptions(string contextName = null,
            string infoMessage = null)
        {
            int books = 0;// (_bookRepo.GetEntities())?.Count() ?? 0;
            int categories = 0;// (_categoryRepo.GetEntities())?.Count() ?? 0;
            int publishers = 0;//(_publisherRepo.GetEntities())?.Count() ?? 0;

            if (!string.IsNullOrEmpty(contextName))
            {
                _manager.ContextName = contextName;
            }
            return new MigrationsOptions(books, categories, publishers, _manager, infoMessage);
        }

        [HttpGet("save")]
        public string SaveDataToJson()
        {
            //IQueryable<Book> dbbooks = _bookRepo.GetEntities().OrderBy(b => b.Id);

            //IQueryable<Category> dbparents = _categoryRepo.GetEntities()
            //    .Where(p => p.ParentCategoryID == null).OrderBy(p => p.Id);

            //IQueryable<Category> dbcategories = _categoryRepo.GetEntities()
            //    .Where(c => c.ParentCategoryID != null).OrderBy(c => c.Id);

            //IQueryable<Publisher> dbpublishers = _publisherRepo.GetEntities().OrderBy(p => p.Id);

            //StoreSavedData storeSavedData = new StoreSavedData
            //{
            //    Books = dbbooks.ToList(),
            //    ParentCategories = dbparents.ToList(),
            //    Categories = dbcategories.ToList(),
            //    Publishers = dbpublishers.ToList()
            //};

            //storeSavedData.Books.ForEach(b => 
            //{
            //    b.Id = 0; b.Category = null; b.Publisher = null;
            //});
            //storeSavedData.ParentCategories.ForEach(p =>
            //{
            //    p.Books = null;
            //    p.ChildrenCategories = null;
            //});
            //storeSavedData.Categories.ForEach(c =>
            //{
            //    c.ParentCategory = null;
            //    c.ChildrenCategories = null;
            //    c.Books = null;
            //});
            //storeSavedData.Publishers.ForEach(p => p.Books = null);
            //DataRW dataRW = new DataRW();

            string msg = string.Empty;
            try
            {
                //dataRW.CreateJsonData(storeSavedData, "savedData");

                msg = "Данные сохранены в файл";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        [HttpGet("seed/{contextName}/{fromFile}")]
        public string SeedDatabase(string contextName, bool fromFile)
        {
            _manager.ContextName = contextName;
            string msg = SeedData.SeedDatabase(_manager.Context, fromFile);

            return msg;
        }

        [HttpGet("clear/{contextName}")]
        public string ClearDatabase(string contextName)
        {
            _manager.ContextName = contextName;
            string msg = SeedData.ClearDatabase(_manager.Context);

            return msg;
        }
    }
}
