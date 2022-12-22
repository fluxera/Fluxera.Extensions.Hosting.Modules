using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { "bc04d38d-22ce-4861-98f3-79786ffa8f1e", null, "Deodorant", 5.95m },
                    { "ee48daf6-e983-4b5a-ba11-4d15f0bd3840", null, "Shower Gel", 5.99m },
                    { "f2f764f2-3690-4abb-9bd9-58bc8502919a", null, "Soap", 3.95m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: "bc04d38d-22ce-4861-98f3-79786ffa8f1e");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: "ee48daf6-e983-4b5a-ba11-4d15f0bd3840");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: "f2f764f2-3690-4abb-9bd9-58bc8502919a");
        }
    }
}
