using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationWeather : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Weather",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Weather");
        }
    }
}
