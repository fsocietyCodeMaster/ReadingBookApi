using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingBookApi.Migrations
{
    /// <inheritdoc />
    public partial class adding_averageRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "t_Books",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "t_Books");
        }
    }
}
