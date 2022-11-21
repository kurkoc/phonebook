using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.API.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class pathcolumnsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Report",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Report");
        }
    }
}
