using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Database.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_FetchedDeliveryOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FetchedDeliveryOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    CityRegion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RegionQuery = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstOrderDateQuery = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QueryDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FetchedDeliveryOrders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FetchedDeliveryOrders");
        }
    }
}
