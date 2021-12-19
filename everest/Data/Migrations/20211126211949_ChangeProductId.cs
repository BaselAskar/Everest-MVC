using Microsoft.EntityFrameworkCore.Migrations;

namespace everest.Data.Migrations
{
    public partial class ChangeProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsPhotos_Products_ProductId1",
                table: "ProductsPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProductsPhotos_ProductId1",
                table: "ProductsPhotos");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductsPhotos");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsPhotos_ProductId",
                table: "ProductsPhotos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsPhotos_Products_ProductId",
                table: "ProductsPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsPhotos_Products_ProductId",
                table: "ProductsPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProductsPhotos_ProductId",
                table: "ProductsPhotos");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductsPhotos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsPhotos_ProductId1",
                table: "ProductsPhotos",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsPhotos_Products_ProductId1",
                table: "ProductsPhotos",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
