using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaripSozluk.Data.Migrations
{
    public partial class fromidtoidcodefornavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdCode",
                table: "Headers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdCode",
                table: "Categories",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "90e12d5d-cf32-44d0-90a7-7ee344106f75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f0207b64-5e68-4b1f-99cc-6f62144f0036");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5738724c-9eda-44c1-90b7-eb5bf930e199");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 10, 22, 23, 54, 44, 63, DateTimeKind.Local).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "BirthDate", "CreateDate" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 65, DateTimeKind.Local).AddTicks(5575), new DateTime(2020, 10, 22, 23, 54, 44, 65, DateTimeKind.Local).AddTicks(5619) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "IdCode" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 65, DateTimeKind.Local).AddTicks(9704), "gundem" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "IdCode" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 66, DateTimeKind.Local).AddTicks(992), "debe" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "IdCode" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 66, DateTimeKind.Local).AddTicks(1015), "sorunsallar" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "IdCode" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 66, DateTimeKind.Local).AddTicks(1016), "spor" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "IdCode" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 66, DateTimeKind.Local).AddTicks(1018), "iliskiler" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "IdCode" },
                values: new object[] { new DateTime(2020, 10, 22, 23, 54, 44, 66, DateTimeKind.Local).AddTicks(1023), "siyaset" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCode",
                table: "Headers");

            migrationBuilder.DropColumn(
                name: "IdCode",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "77907242-0a05-44a3-a8fd-11f9c7ea55c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "170af3e6-1845-4573-81b8-3bcff2393c59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "00afa7c3-082e-4430-9f50-05ccca48feaf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 111, DateTimeKind.Local).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "BirthDate", "CreateDate" },
                values: new object[] { new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(3196), new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(3243) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(7131));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(7985));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2020, 10, 20, 16, 51, 11, 114, DateTimeKind.Local).AddTicks(7989));
        }
    }
}
