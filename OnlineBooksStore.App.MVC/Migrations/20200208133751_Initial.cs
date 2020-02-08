using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBooksStore.App.MVC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Authors = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    PageCount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    RetailPrice = table.Column<decimal>(nullable: false),
                    BookCover = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
