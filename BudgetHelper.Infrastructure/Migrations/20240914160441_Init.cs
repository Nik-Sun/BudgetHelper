using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetHelper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Домашни любимци" },
                    { 2, "Битови сметки" },
                    { 3, "Спорт" },
                    { 4, "Домашни потерби" },
                    { 5, "Хранителни стоки" },
                    { 6, "Ресторанти и развлечения" },
                    { 7, "Здраве" },
                    { 8, "Красота" },
                    { 9, "Авто разходи" },
                    { 10, "Подаръци" },
                    { 11, "Други разходи" },

                });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Електричество" },
                    { 2, 2, "Вода" },
                    { 3, 2, "Телефони" },
                    { 4, 2, "Интернет и ТВ" },
                    { 5, 2, "Spotify" },
                    { 6, 2, "Топлофикация" },
                    { 7, 2, "Такса вход" },
                    { 8, 5, "Lidl" },
                    { 9, 5, "Kaufland" },
                    { 10, 5, "Фантастико" },
                    { 11, 5, "ЦБА" },
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TypeId",
                table: "Expenses",
                column: "TypeId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_CategoryId",
                table: "ExpenseTypes",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
