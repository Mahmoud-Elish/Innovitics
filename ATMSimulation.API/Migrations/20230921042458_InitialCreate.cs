using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATMSimulation.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PIN = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.CardNumber);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "CardNumber", "Balance", "Name", "PIN" },
                values: new object[,]
                {
                    { "12345678901234", 9100, "Radwa", "000000" },
                    { "12345678955555", 8000, "Mahmoud Elish", "123456" },
                    { "56789012345678", 2500, "Ali", "666666" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
