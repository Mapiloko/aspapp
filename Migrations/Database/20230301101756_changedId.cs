using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspApp.Migrations.Database
{
    public partial class changedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderUniqueId",
                table: "Orders",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderUniqueId");
        }
    }
}
