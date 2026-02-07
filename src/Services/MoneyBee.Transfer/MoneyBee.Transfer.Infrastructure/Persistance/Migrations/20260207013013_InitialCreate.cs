using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoneyBee.Transfer.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    AmountInTry = table.Column<decimal>(type: "numeric", nullable: false),
                    Fee = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TransactionCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Transfers",
                columns: new[] { "Id", "Amount", "AmountInTry", "ApprovedDate", "CreatedBy", "CreatedDate", "Currency", "DeletedDate", "Fee", "IsDeleted", "ReceiverCustomerId", "SenderCustomerId", "Status", "TransactionCode", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("b11111e9-2ba9-473a-a40f-e38cb54f9b35"), 100.00m, 3300.00m, new DateTime(2026, 2, 7, 1, 30, 13, 209, DateTimeKind.Utc).AddTicks(730), "System", new DateTime(2026, 2, 7, 1, 30, 13, 209, DateTimeKind.Utc).AddTicks(730), "USD", null, 1.50m, false, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "COMPLETED", "TX-USD-2026-001", null, null },
                    { new Guid("c22222e9-2ba9-473a-a40f-e38cb54f9b35"), 500.00m, 500.00m, null, "System", new DateTime(2026, 2, 7, 1, 30, 13, 209, DateTimeKind.Utc).AddTicks(740), "TRY", null, 5.00m, false, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "PENDING", "TX-TRY-2026-002", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TransactionCode",
                table: "Transfers",
                column: "TransactionCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
