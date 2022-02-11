using Microsoft.EntityFrameworkCore.Migrations;

namespace everest.Data.Migrations
{
    public partial class ChangeClassificatios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassificationClinic");

            migrationBuilder.DropTable(
                name: "ClassificationStore");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Stores",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Clinics",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ClinicClassifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreClassifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicClinicClassification",
                columns: table => new
                {
                    ClassificationsId = table.Column<string>(type: "TEXT", nullable: false),
                    ClinicsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicClinicClassification", x => new { x.ClassificationsId, x.ClinicsId });
                    table.ForeignKey(
                        name: "FK_ClinicClinicClassification_ClinicClassifications_ClassificationsId",
                        column: x => x.ClassificationsId,
                        principalTable: "ClinicClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicClinicClassification_Clinics_ClinicsId",
                        column: x => x.ClinicsId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreStoreClassification",
                columns: table => new
                {
                    ClassificationsId = table.Column<string>(type: "TEXT", nullable: false),
                    StoresId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStoreClassification", x => new { x.ClassificationsId, x.StoresId });
                    table.ForeignKey(
                        name: "FK_StoreStoreClassification_StoreClassifications_ClassificationsId",
                        column: x => x.ClassificationsId,
                        principalTable: "StoreClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStoreClassification_Stores_StoresId",
                        column: x => x.StoresId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicClinicClassification_ClinicsId",
                table: "ClinicClinicClassification",
                column: "ClinicsId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStoreClassification_StoresId",
                table: "StoreStoreClassification",
                column: "StoresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicClinicClassification");

            migrationBuilder.DropTable(
                name: "StoreStoreClassification");

            migrationBuilder.DropTable(
                name: "ClinicClassifications");

            migrationBuilder.DropTable(
                name: "StoreClassifications");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Clinics");

            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassificationClinic",
                columns: table => new
                {
                    ClassificationsId = table.Column<string>(type: "TEXT", nullable: false),
                    ClinicsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationClinic", x => new { x.ClassificationsId, x.ClinicsId });
                    table.ForeignKey(
                        name: "FK_ClassificationClinic_Classifications_ClassificationsId",
                        column: x => x.ClassificationsId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassificationClinic_Clinics_ClinicsId",
                        column: x => x.ClinicsId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassificationStore",
                columns: table => new
                {
                    ClassificationsId = table.Column<string>(type: "TEXT", nullable: false),
                    StoresId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationStore", x => new { x.ClassificationsId, x.StoresId });
                    table.ForeignKey(
                        name: "FK_ClassificationStore_Classifications_ClassificationsId",
                        column: x => x.ClassificationsId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassificationStore_Stores_StoresId",
                        column: x => x.StoresId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationClinic_ClinicsId",
                table: "ClassificationClinic",
                column: "ClinicsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationStore_StoresId",
                table: "ClassificationStore",
                column: "StoresId");
        }
    }
}
