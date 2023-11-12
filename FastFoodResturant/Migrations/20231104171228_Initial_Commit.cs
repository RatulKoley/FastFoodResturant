using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodResturant.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Commit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CategoryImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "gst_master",
                columns: table => new
                {
                    GSTId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GSTName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GSTPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gst_master", x => x.GSTId);
                });

            migrationBuilder.CreateTable(
                name: "supplier_master",
                columns: table => new
                {
                    SupplierId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplierPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier_master", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "table_master",
                columns: table => new
                {
                    TableId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FloorType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    IsOutSide = table.Column<bool>(type: "bit", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_master", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "unit_master",
                columns: table => new
                {
                    UnitId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_master", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "food_master",
                columns: table => new
                {
                    FoodMasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    GSTId = table.Column<long>(type: "bigint", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FoodType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FoodDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    FoodImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FoodPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreparationTime = table.Column<int>(type: "int", nullable: false),
                    IsSpicy = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: true),
                    Carbohydrates = table.Column<int>(type: "int", nullable: true),
                    Fat = table.Column<int>(type: "int", nullable: true),
                    Fiber = table.Column<int>(type: "int", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_master", x => x.FoodMasterId);
                    table.ForeignKey(
                        name: "FK_FoodMaster_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FoodMaster_GSTMaster",
                        column: x => x.GSTId,
                        principalTable: "gst_master",
                        principalColumn: "GSTId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "purchase_product_master",
                columns: table => new
                {
                    ProductPurchaseMasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdjustAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_product_master", x => x.ProductPurchaseMasterId);
                    table.ForeignKey(
                        name: "FK_PurchaseProductMaster_SupplierMaster",
                        column: x => x.SupplierId,
                        principalTable: "supplier_master",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "product_master",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    StockId = table.Column<long>(type: "bigint", nullable: false),
                    GSTId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_master", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductMaster_GSTMaster",
                        column: x => x.GSTId,
                        principalTable: "gst_master",
                        principalColumn: "GSTId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductMaster_UnitMaster",
                        column: x => x.UnitId,
                        principalTable: "unit_master",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "toppings_master",
                columns: table => new
                {
                    ToppingsMasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    GSTId = table.Column<long>(type: "bigint", nullable: false),
                    FoodMasterId = table.Column<long>(type: "bigint", nullable: false),
                    ToppingsName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToppingsDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ToppingsImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ToppingsPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSpicy = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toppings_master", x => x.ToppingsMasterId);
                    table.ForeignKey(
                        name: "FK_ToppingsMaster_Category",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ToppingsMaster_FoodMaster",
                        column: x => x.FoodMasterId,
                        principalTable: "food_master",
                        principalColumn: "FoodMasterId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ToppingsMaster_GSTMaster",
                        column: x => x.GSTId,
                        principalTable: "gst_master",
                        principalColumn: "GSTId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "product_stock",
                columns: table => new
                {
                    ProductStockId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_stock", x => x.ProductStockId);
                    table.ForeignKey(
                        name: "FK_ProductStock_ProductMaster",
                        column: x => x.ProductId,
                        principalTable: "product_master",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "purchase_product_details",
                columns: table => new
                {
                    ProductPurchaseDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPurchaseMasterId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    ItemQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerItemMRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerItemTradeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_product_details", x => x.ProductPurchaseDetailsId);
                    table.ForeignKey(
                        name: "FK_PurchaseProductDetails_ProductMaster",
                        column: x => x.ItemId,
                        principalTable: "product_master",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PurchaseProductDetails_PurchaseProductMaster",
                        column: x => x.ProductPurchaseMasterId,
                        principalTable: "purchase_product_master",
                        principalColumn: "ProductPurchaseMasterId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_CategoryId_IsAvailable",
                table: "category",
                columns: new[] { "CategoryId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_master_CategoryId_IsAvailable",
                table: "food_master",
                columns: new[] { "CategoryId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_master_FoodMasterId_IsAvailable",
                table: "food_master",
                columns: new[] { "FoodMasterId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_master_GSTId_IsAvailable",
                table: "food_master",
                columns: new[] { "GSTId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gst_master_GSTId_IsAvailable",
                table: "gst_master",
                columns: new[] { "GSTId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_master_GSTId_IsAvailable",
                table: "product_master",
                columns: new[] { "GSTId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_master_ProductId_IsAvailable",
                table: "product_master",
                columns: new[] { "ProductId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_master_StockId_IsAvailable",
                table: "product_master",
                columns: new[] { "StockId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_master_UnitId_IsAvailable",
                table: "product_master",
                columns: new[] { "UnitId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_stock_ProductId",
                table: "product_stock",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_stock_ProductId_IsAvailable",
                table: "product_stock",
                columns: new[] { "ProductId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_stock_ProductStockId_IsAvailable",
                table: "product_stock",
                columns: new[] { "ProductStockId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_product_details_ItemId_IsAvailable",
                table: "purchase_product_details",
                columns: new[] { "ItemId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_product_details_ProductPurchaseDetailsId_IsAvailable",
                table: "purchase_product_details",
                columns: new[] { "ProductPurchaseDetailsId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_product_details_ProductPurchaseMasterId_IsAvailable",
                table: "purchase_product_details",
                columns: new[] { "ProductPurchaseMasterId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_product_master_ProductPurchaseMasterId_IsAvailable",
                table: "purchase_product_master",
                columns: new[] { "ProductPurchaseMasterId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_product_master_SupplierId_IsAvailable",
                table: "purchase_product_master",
                columns: new[] { "SupplierId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_supplier_master_SupplierId_IsAvailable",
                table: "supplier_master",
                columns: new[] { "SupplierId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_table_master_TableId_IsAvailable",
                table: "table_master",
                columns: new[] { "TableId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_toppings_master_CategoryId_IsAvailable",
                table: "toppings_master",
                columns: new[] { "CategoryId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_toppings_master_FoodMasterId_IsAvailable",
                table: "toppings_master",
                columns: new[] { "FoodMasterId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_toppings_master_GSTId_IsAvailable",
                table: "toppings_master",
                columns: new[] { "GSTId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_toppings_master_ToppingsMasterId_IsAvailable",
                table: "toppings_master",
                columns: new[] { "ToppingsMasterId", "IsAvailable" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_unit_master_UnitId_IsAvailable",
                table: "unit_master",
                columns: new[] { "UnitId", "IsAvailable" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_stock");

            migrationBuilder.DropTable(
                name: "purchase_product_details");

            migrationBuilder.DropTable(
                name: "table_master");

            migrationBuilder.DropTable(
                name: "toppings_master");

            migrationBuilder.DropTable(
                name: "product_master");

            migrationBuilder.DropTable(
                name: "purchase_product_master");

            migrationBuilder.DropTable(
                name: "food_master");

            migrationBuilder.DropTable(
                name: "unit_master");

            migrationBuilder.DropTable(
                name: "supplier_master");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "gst_master");
        }
    }
}
