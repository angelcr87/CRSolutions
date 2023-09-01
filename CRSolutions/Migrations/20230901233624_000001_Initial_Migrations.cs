using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRSolutions.Migrations
{
    public partial class _000001_Initial_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    CompanyDescription = table.Column<string>(type: "VARCHAR(500)", maxLength: 500, nullable: false),
                    BusinessName = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    RFC = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Credits = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.IdCompany);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(500)", maxLength: 500, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IdRol = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleIdRol = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyIdCompany",
                        column: x => x.CompanyIdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleIdRol",
                        column: x => x.RoleIdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    IdCantidate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluatedPosition = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    IdRiskScore = table.Column<int>(type: "int", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportFile = table.Column<string>(type: "VARCHAR(600)", maxLength: 600, nullable: false),
                    AudioFile = table.Column<string>(type: "VARCHAR(600)", maxLength: 600, nullable: false),
                    PhotoFile = table.Column<string>(type: "VARCHAR(600)", maxLength: 600, nullable: false),
                    IdTypeTest = table.Column<int>(type: "int", nullable: false),
                    RecordEvaluation = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    BlackList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserIdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyIdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.IdCantidate);
                    table.ForeignKey(
                        name: "FK_Candidates_Companies_CompanyIdCompany",
                        column: x => x.CompanyIdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Users_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CompanyIdCompany",
                table: "Candidates",
                column: "CompanyIdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UserIdUser",
                table: "Candidates",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyIdCompany",
                table: "Users",
                column: "CompanyIdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleIdRol",
                table: "Users",
                column: "RoleIdRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
