using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRSolutions.Migrations
{
    public partial class _000002_Modified_filed_CreditFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoFile",
                table: "Candidates",
                newName: "CreditFile");

            migrationBuilder.AddColumn<string>(
                name: "CURP",
                table: "Candidates",
                type: "VARCHAR(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CURP",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "CreditFile",
                table: "Candidates",
                newName: "PhotoFile");
        }
    }
}
