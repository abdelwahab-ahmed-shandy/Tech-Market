using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tech_Mart.Migrations
{
    /// <inheritdoc />
    public partial class CreateDBAndEnterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quntity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "CategoryImg", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "mobiles.jpg", "Mobile Products", "Mobiles" },
                    { 2, "laptops.jpg", "LabTops Products", "LabTops" },
                    { 3, "tablets.jpg", "Tablets Products", "Tablets" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CategoryId", "Description", "Discount", "Img", "Name", "Price", "Quntity", "Rate" },
                values: new object[,]
                {
                    { 1, 1, "Flagship smartphone from Apple", 0.10000000000000001, "Iphone11.jpg", "Iphone11", 1999.99, 10, 4.5 },
                    { 2, 1, "High-performance smartphone from Apple", 0.10000000000000001, "Iphone12.jpg", "Iphone12", 2999.9899999999998, 10, 4.5 },
                    { 3, 1, "Latest smartphone from Apple", 0.10000000000000001, "Iphone13.jpg", "Iphone13", 3999.9899999999998, 10, 4.5 },
                    { 4, 1, "Premium smartphone from Apple", 0.10000000000000001, "Iphone14.jpg", "Iphone14", 4999.9899999999998, 10, 4.5 },
                    { 5, 1, "Next-gen smartphone from Apple", 0.10000000000000001, "Iphone15.jpg", "Iphone15", 5999.9899999999998, 10, 4.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
