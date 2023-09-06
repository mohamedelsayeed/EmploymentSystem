using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentSystem.Persistance.Migrations
{
    public partial class add_IsArchievedField_toVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Vacancies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Vacancies");
        }
    }
}
