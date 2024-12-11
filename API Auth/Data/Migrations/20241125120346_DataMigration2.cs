using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Auth.Migrations
{
    /// <inheritdoc />
    public partial class DataMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Suppliers_SupplierId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "EmployeeTimesheet");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SupplierId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Timesheets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "invoiceId",
                table: "Suppliers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                column: "invoiceId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2,
                column: "invoiceId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_EmployeeId",
                table: "Timesheets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_invoiceId",
                table: "Suppliers",
                column: "invoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Invoices_invoiceId",
                table: "Suppliers",
                column: "invoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Employees_EmployeeId",
                table: "Timesheets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Invoices_invoiceId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Employees_EmployeeId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_EmployeeId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_invoiceId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "invoiceId",
                table: "Suppliers");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Timesheets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Invoices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Invoices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeTimesheet",
                columns: table => new
                {
                    EmployeeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimesheetsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTimesheet", x => new { x.EmployeeeId, x.TimesheetsId });
                    table.ForeignKey(
                        name: "FK_EmployeeTimesheet_Employees_EmployeeeId",
                        column: x => x.EmployeeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTimesheet_Timesheets_TimesheetsId",
                        column: x => x.TimesheetsId,
                        principalTable: "Timesheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SupplierId",
                table: "Invoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTimesheet_TimesheetsId",
                table: "EmployeeTimesheet",
                column: "TimesheetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Suppliers_SupplierId",
                table: "Invoices",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
