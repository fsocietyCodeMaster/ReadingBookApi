using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingBookApi.Migrations
{
    /// <inheritdoc />
    public partial class adding_username_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "t_Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "t_Review");
        }
    }
}
