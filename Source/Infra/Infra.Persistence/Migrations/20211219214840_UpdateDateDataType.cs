using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Persistence.Migrations
{
    public partial class UpdateDateDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Transactions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Transactions",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
