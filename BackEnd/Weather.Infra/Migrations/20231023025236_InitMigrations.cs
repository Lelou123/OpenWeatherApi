using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    TemperatureMin = table.Column<double>(type: "float", nullable: false),
                    TemperatureMax = table.Column<double>(type: "float", nullable: false),
                    FeelsLike = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    WeatherId = table.Column<int>(type: "int", nullable: false),
                    WeatherMain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    CloudsAll = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RainVolume = table.Column<double>(type: "float", nullable: true),
                    Pop = table.Column<double>(type: "float", nullable: true),
                    SeaLevel = table.Column<int>(type: "int", nullable: true),
                    GroundLevel = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weather_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weather_LocationId",
                table: "Weather",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
