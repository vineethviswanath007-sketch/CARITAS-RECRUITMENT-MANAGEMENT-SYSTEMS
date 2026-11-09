using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARITAS_RECRUITMENT_MANAGEMENT_SYSTEMS.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidatePersonalDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caste",
                table: "Candidates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastWorkedOrganization",
                table: "Candidates",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Candidates",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "Candidates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Caste",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LastWorkedOrganization",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Candidates");
        }
    }
}
