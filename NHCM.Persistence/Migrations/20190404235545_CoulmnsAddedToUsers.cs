using Microsoft.EntityFrameworkCore.Migrations;

namespace NHCM.Persistence.Migrations
{
    public partial class CoulmnsAddedToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OrganizationAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SuperAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

           // migrationBuilder.AddColumn<int>(name:"EmployeeID", table:"AspNetUsers",  nullable: true, defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperAdmin",
                table: "AspNetUsers");
        }
    }
}
