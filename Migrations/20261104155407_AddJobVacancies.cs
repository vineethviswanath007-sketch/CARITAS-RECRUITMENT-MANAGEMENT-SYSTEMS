using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARITAS_RECRUITMENT_MANAGEMENT_SYSTEMS.Migrations
{
    /// <inheritdoc />
    public partial class AddJobVacancies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobVacancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ExperienceRequired = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfVacancies = table.Column<int>(type: "int", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SalaryRange = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobVacancies_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancies_DepartmentId",
                table: "JobVacancies",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobVacancies");
        }
    }
}
