using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentSystem.Persistance.Migrations;

public partial class add_Vacancy_and_Application : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Applications",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ApplicantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                VacancyId = table.Column<int>(type: "int", nullable: false),
                ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Applications", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Vacancies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MaxApplications = table.Column<int>(type: "int", nullable: false),
                CurrentApplications = table.Column<int>(type: "int", nullable: false),
                IsClosed = table.Column<bool>(type: "bit", nullable: false),
                ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EmployerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vacancies", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Applications");

        migrationBuilder.DropTable(
            name: "Vacancies");
    }
}
