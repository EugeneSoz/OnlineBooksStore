using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Persistence.EF;
using OnlineBooksStore.Persistence.EF.Mvc;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class SeedController : Controller
    {
        private readonly DataContext _context;

        public SeedController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IActionResult Index()
        {
            ViewBag.Count = _context.Books.Count();
            return View(_context.Books
                .Include(book => book.Category)
                .Include(book => book.Publisher).OrderBy(p => p.Id).Take(20));
        }

        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                _context.Database
                    .ExecuteSqlCommand("DROP PROCEDURE IF EXISTS CreateSeedData");
                _context.Database.ExecuteSqlCommand($@"
                    CREATE PROCEDURE CreateSeedData @RowCount DECIMAL
                    AS
                    BEGIN
                        SET NOCOUNT ON
                        DECLARE @i INT = 1;
                        DECLARE @catId BIGINT;
                        DECLARE @pubId BIGINT;
                        DECLARE @CatCount INT = @RowCount / 10;
                        DECLARE @PupCount INT = @RowCount / 10;
                        DECLARE @pprice DECIMAL(5, 2);
                        DECLARE @rprice DECIMAL(5, 2);
                        BEGIN TRANSACTION
                            WHILE @i <= @CatCount
                                BEGIN
                                    INSERT INTO Categories (NameEng)
                                    VALUES (CONCAT('Category-', @i));
                                    SET @catId = SCOPE_IDENTITY();

                                    INSERT INTO Publishers (Name, Country)
                                    VALUES (CONCAT('Publisher-', @i), 'Russia')
                                    SET @pubId = SCOPE_IDENTITY();

                                    DECLARE @j INT = 1;
                                    WHILE @j <= 10
                                        BEGIN
                                            SET @pprice = RAND() * (500 - 5 + 1);
                                            SET @rprice = (RAND() * @pprice)
                                                + @pprice;
                                            INSERT INTO Books (Title, CategoryId, PublisherId, Year, PageCount,
                                              PurchasePrice, RetailPrice)
                                            VALUES (CONCAT('Book', @i, '-', @j),
                                                    @catId, @pubId, 2020, 450, @pprice, @rprice)
                                            SET @j = @j + 1
                                        END
                                    SET @i = @i + 1
                                END
                        COMMIT
                    END");
                _context.Database.BeginTransaction();
                _context.Database.ExecuteSqlCommand($"EXEC CreateSeedData @RowCount = {count}");
                _context.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearData()
        {
            _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
            _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlCommand("DELETE FROM Orders");
            _context.Database.ExecuteSqlCommand("DELETE FROM Categories");
            _context.Database.CommitTransaction();

            return RedirectToAction(nameof(Index));
        }
    }
}