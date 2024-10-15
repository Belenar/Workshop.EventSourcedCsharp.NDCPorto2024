using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read
{
    /// <inheritdoc />
    public partial class CreateReadDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxStatusses",
                columns: table => new
                {
                    BoxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BottleCapacity = table.Column<int>(type: "int", nullable: false),
                    BottleCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxStatusses", x => x.BoxId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxStatusses");
        }
    }
}
