using Microsoft.EntityFrameworkCore.Migrations;

namespace InfrastructureLibrary.Migrations
{
    public partial class reafctorRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.DropIndex(
                name: "IX_Membership_EmployeeId",
                table: "Membership");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Membership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                columns: new[] { "EmployeeId", "GroupId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Membership",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_EmployeeId",
                table: "Membership",
                column: "EmployeeId");
        }
    }
}
