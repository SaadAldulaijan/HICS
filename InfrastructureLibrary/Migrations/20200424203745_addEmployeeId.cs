using Microsoft.EntityFrameworkCore.Migrations;

namespace InfrastructureLibrary.Migrations
{
    public partial class addEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Activation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Activation");
        }
    }
}
