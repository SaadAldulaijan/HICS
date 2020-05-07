using Microsoft.EntityFrameworkCore.Migrations;

namespace InfrastructureLibrary.Migrations
{
    public partial class CodeGroupRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CodeGroup",
                table: "CodeGroup");

            migrationBuilder.DropIndex(
                name: "IX_CodeGroup_GroupId",
                table: "CodeGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CodeGroup");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CodeGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodeGroup",
                table: "CodeGroup",
                columns: new[] { "GroupId", "CodeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CodeGroup",
                table: "CodeGroup");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CodeGroup",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CodeGroup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodeGroup",
                table: "CodeGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CodeGroup_GroupId",
                table: "CodeGroup",
                column: "GroupId");
        }
    }
}
