using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Inıt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technology_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 1, true, "C#" });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 2, true, "Java" });

            migrationBuilder.InsertData(
                table: "Technology",
                columns: new[] { "Id", "IsActive", "Name", "ProgrammingLanguageId" },
                values: new object[] { 1, true, ".NET", 1 });

            migrationBuilder.InsertData(
                table: "Technology",
                columns: new[] { "Id", "IsActive", "Name", "ProgrammingLanguageId" },
                values: new object[] { 2, true, "Technology", 2 });

            migrationBuilder.InsertData(
                table: "Technology",
                columns: new[] { "Id", "IsActive", "Name", "ProgrammingLanguageId" },
                values: new object[] { 3, true, "MVC", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Technology_ProgrammingLanguageId",
                table: "Technology",
                column: "ProgrammingLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Technology");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");
        }
    }
}
