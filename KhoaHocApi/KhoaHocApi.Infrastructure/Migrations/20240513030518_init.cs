using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhoaHocApi.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegíterTime",
                table: "RegisterStudies",
                newName: "RegisterTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterTime",
                table: "RegisterStudies",
                newName: "RegíterTime");
        }
    }
}
