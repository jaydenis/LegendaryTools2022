using Microsoft.EntityFrameworkCore.Migrations;

namespace LegendaryDataCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    SetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Set = table.Column<string>(type: "TEXT", nullable: true),
                    Owned = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.SetId);
                });

            migrationBuilder.CreateTable(
                name: "Henchmen",
                columns: table => new
                {
                    HenchmenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetId = table.Column<int>(type: "INTEGER", nullable: true),
                    HenchmenName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Henchmen", x => x.HenchmenId);
                    table.ForeignKey(
                        name: "FK_Henchmen_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "SetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetId = table.Column<int>(type: "INTEGER", nullable: true),
                    HeroName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.HeroId);
                    table.ForeignKey(
                        name: "FK_Heroes_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "SetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Masterminds",
                columns: table => new
                {
                    MastermindId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetId = table.Column<int>(type: "INTEGER", nullable: true),
                    MastermindName = table.Column<string>(type: "TEXT", nullable: true),
                    Henchman = table.Column<string>(type: "TEXT", nullable: true),
                    Villain = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masterminds", x => x.MastermindId);
                    table.ForeignKey(
                        name: "FK_Masterminds_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "SetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    SchemeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetId = table.Column<int>(type: "INTEGER", nullable: true),
                    HenchmenName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.SchemeId);
                    table.ForeignKey(
                        name: "FK_Schemes_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "SetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Villains",
                columns: table => new
                {
                    VillainId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetId = table.Column<int>(type: "INTEGER", nullable: true),
                    VillainName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villains", x => x.VillainId);
                    table.ForeignKey(
                        name: "FK_Villains_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "SetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Henchmen_SetId",
                table: "Henchmen",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_SetId",
                table: "Heroes",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Masterminds_SetId",
                table: "Masterminds",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_SetId",
                table: "Schemes",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Villains_SetId",
                table: "Villains",
                column: "SetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Henchmen");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Masterminds");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "Villains");

            migrationBuilder.DropTable(
                name: "Sets");
        }
    }
}
