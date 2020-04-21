using Microsoft.EntityFrameworkCore.Migrations;

namespace InfrastructureLibrary.Migrations
{
    public partial class AddDeviceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    MACAddress = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    HostName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.MACAddress);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
