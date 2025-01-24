using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apiLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    queryString = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    payload = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    method = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    response = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    responseCode = table.Column<int>(type: "int", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apiLogs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apiLogs");
        }
    }
}
