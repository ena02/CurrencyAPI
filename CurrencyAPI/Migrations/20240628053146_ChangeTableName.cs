using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyAPI.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "R_CURRENCY");

            migrationBuilder.AddPrimaryKey(
                name: "PK_R_CURRENCY",
                table: "R_CURRENCY",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_R_CURRENCY",
                table: "R_CURRENCY");

            migrationBuilder.RenameTable(
                name: "R_CURRENCY",
                newName: "Currencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");
        }
    }
}
