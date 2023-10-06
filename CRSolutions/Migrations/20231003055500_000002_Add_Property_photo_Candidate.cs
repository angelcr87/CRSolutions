using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRSolutions.Migrations
{
    public partial class _000002_Add_Property_photo_Candidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: new Guid("15fe19da-3dec-4d15-9a63-74bed4a038d3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: new Guid("5a689fe8-5bc5-481f-a1ca-399562d6da2c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: new Guid("85535ac0-2b9c-4f52-994c-2af88799d605"));

            migrationBuilder.AlterColumn<string>(
                name: "RecordEvaluation",
                table: "Candidates",
                type: "VARCHAR(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "BlackList",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Candidates",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "FullName", "IdCompany", "IdRol", "Password", "Status", "UserName" },
                values: new object[] { new Guid("4e63f645-8231-459f-8751-c02dd3e18d6a"), "Administrador", new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), new Guid("4a74da66-bcd1-4662-8625-cb7c3bf2a837"), "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "FullName", "IdCompany", "IdRol", "Password", "Status", "UserName" },
                values: new object[] { new Guid("78b74e3d-136f-4355-ad85-126813b2d872"), "Cliente", new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), new Guid("882e1047-29e1-4276-8bce-6f2372670ae1"), "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", true, "Cliente" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "FullName", "IdCompany", "IdRol", "Password", "Status", "UserName" },
                values: new object[] { new Guid("fef19423-0b87-4137-b42b-9ab3937fea0a"), "Cliente Administrador", new Guid("127cfef3-3e5d-4b47-b58d-19adb61cf6be"), new Guid("789a3411-ed70-42bd-9681-b0d9ae800583"), "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", true, "Cliente_Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: new Guid("4e63f645-8231-459f-8751-c02dd3e18d6a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: new Guid("78b74e3d-136f-4355-ad85-126813b2d872"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: new Guid("fef19423-0b87-4137-b42b-9ab3937fea0a"));

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "RecordEvaluation",
                table: "Candidates",
                type: "VARCHAR(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BlackList",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
