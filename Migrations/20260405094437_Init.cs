using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AbobaWH.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ITN = table.Column<long>(type: "bigint", nullable: false),
                    BIC = table.Column<int>(type: "integer", nullable: false),
                    SettlementAccount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    DirectorFullName = table.Column<string>(type: "text", nullable: false),
                    AccountantFullName = table.Column<string>(type: "text", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    HashedPass = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    CriticalBalance = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    ContractId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractItem_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryScheduleEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ContractId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryScheduleEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryScheduleEntry_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryScheduleEntry_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    ShipmentOrderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentEntry_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentEntry_ShipmentOrder_ShipmentOrderId",
                        column: x => x.ShipmentOrderId,
                        principalTable: "ShipmentOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptOrderEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScheduledDeliveryId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ReceiptOrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptOrderEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptOrderEntry_DeliveryScheduleEntry_ScheduledDeliveryId",
                        column: x => x.ScheduledDeliveryId,
                        principalTable: "DeliveryScheduleEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptOrderEntry_ReceiptOrder_ReceiptOrderId",
                        column: x => x.ReceiptOrderId,
                        principalTable: "ReceiptOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProviderId",
                table: "Contract",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItem_ContractId",
                table: "ContractItem",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractItem_ProductId",
                table: "ContractItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryScheduleEntry_ContractId",
                table: "DeliveryScheduleEntry",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryScheduleEntry_ProductId",
                table: "DeliveryScheduleEntry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitId",
                table: "Product",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptOrderEntry_ReceiptOrderId",
                table: "ReceiptOrderEntry",
                column: "ReceiptOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptOrderEntry_ScheduledDeliveryId",
                table: "ReceiptOrderEntry",
                column: "ScheduledDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentEntry_ProductId",
                table: "ShipmentEntry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentEntry_ShipmentOrderId",
                table: "ShipmentEntry",
                column: "ShipmentOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractItem");

            migrationBuilder.DropTable(
                name: "ReceiptOrderEntry");

            migrationBuilder.DropTable(
                name: "ShipmentEntry");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DeliveryScheduleEntry");

            migrationBuilder.DropTable(
                name: "ReceiptOrder");

            migrationBuilder.DropTable(
                name: "ShipmentOrder");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
