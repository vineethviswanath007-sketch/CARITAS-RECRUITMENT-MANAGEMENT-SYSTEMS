using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARITAS_RECRUITMENT_MANAGEMENT_SYSTEMS.Migrations
{
    public partial class RepairJobApplicationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    ApplicationNumber = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    CandidateId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    JobVacancyId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    ApplicationDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    ResumePath = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    ApplicationStatus = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    InterviewStatus = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    HRRemarks = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_JobApplications",
                        x => x.Id);

                    table.ForeignKey(
                        name: "FK_JobApplications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");

                    table.ForeignKey(
                        name: "FK_JobApplications_JobVacancies_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JobVacancies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CandidateId",
                table: "JobApplications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobVacancyId",
                table: "JobApplications",
                column: "JobVacancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");
        }
    }
}