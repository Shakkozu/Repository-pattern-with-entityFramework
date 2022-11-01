using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework_Playground.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "zoo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zoo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CageSnapshot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Slots = table.Column<int>(type: "INTEGER", nullable: false),
                    OccupiedSlots = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimalSize = table.Column<string>(type: "TEXT", nullable: false),
                    ZooSnapshotId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CageSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CageSnapshot_zoo_ZooSnapshotId",
                        column: x => x.ZooSnapshotId,
                        principalTable: "zoo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnimalSnapshot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Specie = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    CageSnapshotId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalSnapshot_CageSnapshot_CageSnapshotId",
                        column: x => x.CageSnapshotId,
                        principalTable: "CageSnapshot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalSnapshot_CageSnapshotId",
                table: "AnimalSnapshot",
                column: "CageSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_CageSnapshot_ZooSnapshotId",
                table: "CageSnapshot",
                column: "ZooSnapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalSnapshot");

            migrationBuilder.DropTable(
                name: "CageSnapshot");

            migrationBuilder.DropTable(
                name: "zoo");
        }
    }
}
