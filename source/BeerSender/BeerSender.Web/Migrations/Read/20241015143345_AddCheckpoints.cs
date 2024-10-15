using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read
{
    /// <inheritdoc />
    public partial class AddCheckpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectionCheckpoints",
                columns: table => new
                {
                    ProjectionName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EventVersion = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectionCheckpoints", x => x.ProjectionName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectionCheckpoints");
        }
    }
}
