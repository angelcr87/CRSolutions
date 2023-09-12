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
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRol",
                        column: x => x.IdRol,
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
                    CURP = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    EvaluatedPosition = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    IdRiskScore = table.Column<int>(type: "int", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AudioFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreditFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IdTypeTest = table.Column<int>(type: "int", nullable: false),
                    RecordEvaluation = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false),
                    BlackList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.IdCantidate);
                    table.ForeignKey(
                        name: "FK_Candidates_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "IdCompany", "BusinessName", "CompanyDescription", "CompanyName", "Credits", "RFC", "Status" },
                values: new object[] { new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), "razon Social", "Compania Inicial", "CRSolutions", true, "RFC", true });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "Description", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("4a74da66-bcd1-4662-8625-cb7c3bf2a837"), "Puede crear nuevos candidatos", "Admin", true },
                    { new Guid("789a3411-ed70-42bd-9681-b0d9ae800583"), "Puede todos los candidatos", "Cliente Admin", true },
                    { new Guid("882e1047-29e1-4276-8bce-6f2372670ae1"), "puede ver unicamente sus candidatos", "Cliente", true }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "FullName", "IdCompany", "IdRol", "Password", "Status", "UserName" },
                values: new object[] { new Guid("15fe19da-3dec-4d15-9a63-74bed4a038d3"), "Cliente", new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), new Guid("882e1047-29e1-4276-8bce-6f2372670ae1"), "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", true, "Cliente" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "FullName", "IdCompany", "IdRol", "Password", "Status", "UserName" },
                values: new object[] { new Guid("5a689fe8-5bc5-481f-a1ca-399562d6da2c"), "Administrador", new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), new Guid("4a74da66-bcd1-4662-8625-cb7c3bf2a837"), "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "FullName", "IdCompany", "IdRol", "Password", "Status", "UserName" },
                values: new object[] { new Guid("85535ac0-2b9c-4f52-994c-2af88799d605"), "Cliente Administrador", new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), new Guid("789a3411-ed70-42bd-9681-b0d9ae800583"), "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", true, "Cliente_Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_IdCompany",
                table: "Candidates",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_IdUser",
                table: "Candidates",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdCompany",
                table: "Users",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRol",
                table: "Users",
                column: "IdRol");
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
