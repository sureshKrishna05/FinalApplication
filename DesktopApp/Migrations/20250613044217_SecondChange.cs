using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class SecondChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1,
                column: "InvoiceDate",
                value: new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2,
                column: "InvoiceDate",
                value: new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 1,
                column: "InvoiceDate",
                value: new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: 2,
                column: "InvoiceDate",
                value: new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
