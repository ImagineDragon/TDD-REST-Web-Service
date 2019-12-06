using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TDD_REST_Web_Service.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Field1 = table.Column<string>(nullable: true),
                    Field2 = table.Column<int>(nullable: false),
                    Field3 = table.Column<string>(nullable: true),
                    Field4 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultModels");
        }
    }
}
