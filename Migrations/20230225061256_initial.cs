using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AdminTable",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "TestPass1234", " hradmin@test.com" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 3, "department1", "Active" },
                    { 2, 3, "department2", "Inactive" },
                    { 3, 4, "department3", "Inactive" },
                    { 4, 4, "department4", "Inactive" },
                    { 5, 3, "department5", "Active" },
                    { 6, 4, "department6", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "IsManager", "LastName", "ManagerId", "Password", "Status", "Telephone" },
                values: new object[,]
                {
                    { 1, "email1@ggmail.com", "Ketane", false, "Maseloa", 4, "Password123#", "Active", "4456789002" },
                    { 2, "email2@gmail.com", "Marks", false, "Doe", 3, "Password123#", "Inactive", "4456789002" },
                    { 3, "email2@gmail.com", "Smith", true, "Walker", 0, "Password123#", "Active", "4456789002" },
                    { 4, "email3@gmail.com", "Lucky", true, "Dube", 0, "Password123#", "Inactive", "4456789002" },
                    { 5, "email4@gmail.com", "Raymond", false, "Reddd", 4, "Password123#", "Active", "4456789002" },
                    { 6, "email5@gmail.com", "David", false, "Ricks", 4, "Password123#", "Inactive", "4456789002" },
                    { 7, "email6@gmail.com", "Riri", false, "Paris", 3, "Password123#", "Active", "4456789002" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminTable");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
