using ApiToProject.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApiToProject.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Specialization = table.Column<string>(maxLength: 50, nullable: false),
                    YearsOfWork = table.Column<int>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageName = table.Column<string>(maxLength: 20, nullable: false),
                    ReadingLevel = table.Column<int>(nullable: false),
                    SpeakingLevel = table.Column<int>(nullable: false),
                    WritingLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientSector = table.Column<string>(maxLength: 50, nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Technologies = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExperienceInYears = table.Column<double>(nullable: false),
                    Profficiency = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguage_Languages_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguage_Employees_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSkill",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    SkillId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSkill_Skills_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSkill_Employees_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguage_LanguageId",
                table: "EmployeeLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguage_EmployeeId_LanguageId",
                table: "EmployeeLanguage",
                columns: new[] { "EmployeeId", "LanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_EmployeeId",
                table: "EmployeeProject",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectId_EmployeeId",
                table: "EmployeeProject",
                columns: new[] { "ProjectId", "EmployeeId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSkill_SkillId",
                table: "EmployeeSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSkill_EmployeeId_SkillId",
                table: "EmployeeSkill",
                columns: new[] { "EmployeeId", "SkillId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLanguage");

            migrationBuilder.DropTable(
                name: "EmployeeProject");

            migrationBuilder.DropTable(
                name: "EmployeeSkill");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
