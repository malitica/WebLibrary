using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBiblioteka.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    NumOfPages = table.Column<int>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true),
                    TypeOfFile = table.Column<string>(maxLength: 20, nullable: true),
                    Author = table.Column<string>(nullable: false),
                    BookDesc = table.Column<string>(maxLength: 200, nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
