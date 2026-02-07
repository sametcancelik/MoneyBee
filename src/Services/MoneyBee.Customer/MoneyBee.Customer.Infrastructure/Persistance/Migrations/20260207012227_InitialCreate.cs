using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoneyBee.Customer.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NationalId = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    TaxNumber = table.Column<string>(type: "text", nullable: true),
                    IsKycVerified = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CustomerId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DailyTotalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LastTransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLimits_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BirthDate", "CreatedBy", "CreatedDate", "DeletedDate", "Email", "FirstName", "IsDeleted", "IsKycVerified", "LastName", "NationalId", "PhoneNumber", "Status", "TaxNumber", "Type", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(610), null, "samet@moneybee.com", "Samet Can", false, true, "Çelik", "12345678901", "5551112233", 1, null, 1, null, null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new DateTime(1988, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), "System", new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(610), null, "viewer@moneybee.com", "Viewer", false, false, "User", "98765432109", "5559998877", 1, "9998887766", 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "CreatedBy", "CreatedDate", "Currency", "CustomerId", "CustomerId1", "DeletedDate", "IsActive", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("e11111e9-2ba9-473a-a40f-e38cb54f9b35"), "TR990000100012345678901001", 10000.00m, "System", new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(670), "TRY", new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), null, null, true, false, null, null },
                    { new Guid("e22222e9-2ba9-473a-a40f-e38cb54f9b35"), "TR990000100012345678901002", 500.00m, "System", new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(670), "USD", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), null, null, true, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "CustomerLimits",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CustomerId", "DailyTotalAmount", "DeletedDate", "IsDeleted", "LastTransactionDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f11111e9-2ba9-473a-a40f-e38cb54f9b35"), "System", new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(690), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), 10000.00m, null, false, new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(690), null, null },
                    { new Guid("f22222e9-2ba9-473a-a40f-e38cb54f9b35"), "System", new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(690), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), 0.00m, null, false, new DateTime(2026, 2, 7, 1, 22, 27, 609, DateTimeKind.Utc).AddTicks(690), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId1",
                table: "Accounts",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLimits_CustomerId",
                table: "CustomerLimits",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_NationalId",
                table: "Customers",
                column: "NationalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CustomerLimits");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
