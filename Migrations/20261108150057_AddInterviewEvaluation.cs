using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARITAS_RECRUITMENT_MANAGEMENT_SYSTEMS.Migrations
{
    /// <inheritdoc />
    public partial class AddInterviewEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunicationSkillsScore",
                table: "Interviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterviewerFeedback",
                table: "Interviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OverallPerformanceScore",
                table: "Interviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalKnowledgeScore",
                table: "Interviews",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommunicationSkillsScore",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "InterviewerFeedback",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "OverallPerformanceScore",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "TechnicalKnowledgeScore",
                table: "Interviews");
        }
    }
}
