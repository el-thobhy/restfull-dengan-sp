using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiPointOfSales.Migrations
{
    /// <inheritdoc />
    public partial class addseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("" +
                "INSERT INTO Customer (CustId, CustName) VALUES " +
                "('C001', 'Budi'), " +
                "('C002', 'Wahyu')," +
                "('C003', 'Asep') ");

            migrationBuilder.Sql("" +
                "INSERT INTO Price (ProductCode, Price, PriceValidateFrom, PriceValidateTo) VALUES " +
                "('P001',97000, '2024-05-02', '2024-08-12')," +
                "('P002', 109000, '2024-05-02', '2024-08-12')," +
                "('P003', 120000, '2024-05-02', '2024-08-12') ");

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductCode", "ProductName" },
                values: new object[,]
                {
                    { "P001", "Filosofi Teras" },
                    { "P002", "Sepotong Hati Yang Baru" },
                    { "P003", "How To Run Family Business" }
                });

            migrationBuilder.InsertData(
                table: "SalesOrder",
                columns: new[] { "SalesOrderNo", "CustCode", "OrderDate" },
                values: new object[,]
                {
                    { "SO001", "C001", new DateTime(2024, 7, 2, 16, 51, 1, 20, DateTimeKind.Local).AddTicks(6751) },
                    { "SO002", "C002", new DateTime(2024, 7, 2, 16, 51, 1, 20, DateTimeKind.Local).AddTicks(6762) },
                    { "SO003", "C003", new DateTime(2024, 7, 2, 16, 51, 1, 20, DateTimeKind.Local).AddTicks(6763) }
                });

            migrationBuilder.InsertData(
                table: "SalesOrderDetail",
                columns: new[] { "ProductCode", "SalesOrderNo", "Price", "Qty" },
                values: new object[,]
                {
                    { "P001", "SO001", 97000m, 1 },
                    { "P002", "SO002", 109000m, 1 },
                    { "P003", "SO003", 120000m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "SalesOrderDetail");
        }
    }
}
