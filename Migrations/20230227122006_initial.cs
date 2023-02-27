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
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, 4, "dpmt1", "Active" },
                    { 2, 3, "dpmt2", "Inactive" },
                    { 3, 10, "dpmt3", "Inactive" },
                    { 4, 11, "dpmt4", "Inactive" },
                    { 5, 27, "dpmt5", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "IsManager", "LastName", "Password", "Status", "Telephone" },
                values: new object[,]
                {
                    { 1, 1, "email1@ggmail.com", "Ketane", false, "Maseloa", "Password123#", "Active", "4456789002" },
                    { 2, 1, "email2@gmail.com", "Marks", false, "Doe", "Password123#", "Inactive", "4456789002" },
                    { 3, 2, "email2@gmail.com", "SmithE", true, "WalkerMozi", "Password123#", "Active", "4456789002" },
                    { 4, 1, "email3@gmail.com", "Lucky", true, "Dube", "Password123#", "Inactive", "4456789002" },
                    { 5, 1, "email4@gmail.com", "Raymond", false, "Reddd", "Password123#", "Active", "4456789002" },
                    { 6, 2, "email5@gmail.com", "David", false, "Ricks", "Password123#", "Inactive", "4456789002" },
                    { 7, 2, "email6@gmail.com", "Riri", false, "Paris", "Password123#", "Active", "4456789002" },
                    { 8, 2, "email1@ggmail.com", "Thandeka", false, "Keeper", "Password123#", "Active", "4456789002" },
                    { 9, 2, "email2@gmail.com", "Pro", false, "Steve", "Password123#", "Inactive", "4456789002" },
                    { 10, 3, "email2@gmail.com", "Smith", true, "Walker", "Password123#", "Active", "4456789002" },
                    { 11, 4, "email3@gmail.com", "Ompha", true, "Fortunate", "Password123#", "Inactive", "4456789002" },
                    { 12, 3, "email4@gmail.com", "Raymond", false, "Reddd", "Password123#", "Active", "4456789002" },
                    { 13, 3, "email5@gmail.com", "Milly", false, "Thwala", "Password123#", "Inactive", "4456789002" },
                    { 14, 3, "email6@gmail.com", "Selunathi", false, "Muzex", "Password123#", "Active", "4456789002" },
                    { 15, 3, "email1@ggmail.com", "Phindile", false, "Sendisoa", "Password123#", "Active", "4456789002" },
                    { 16, 3, "email2@gmail.com", "Mjeja", false, "Revent", "Password123#", "Inactive", "4456789002" },
                    { 17, 4, "email2@gmail.com", "Letor", false, "Izzu", "Password123#", "Active", "4456789002" },
                    { 18, 4, "email3@gmail.com", "Nikita", false, "Dudu", "Password123#", "Inactive", "4456789002" },
                    { 19, 4, "email4@gmail.com", "Phase", false, "Way", "Password123#", "Active", "4456789002" },
                    { 20, 4, "email5@gmail.com", "Rose", false, "Pink", "Password123#", "Inactive", "4456789002" },
                    { 21, 4, "email6@gmail.com", "Khanya", false, "Phonm", "Password123#", "Active", "4456789002" },
                    { 22, 5, "email2@gmail.com", "Free", false, "Mak", "Password123#", "Inactive", "4456789002" },
                    { 23, 5, "email2@gmail.com", "Chance ", false, "Bruno", "Password123#", "Active", "4456789002" },
                    { 24, 5, "email3@gmail.com", "Tutu", false, "Maplo", "Password123#", "Inactive", "4456789002" },
                    { 25, 5, "email4@gmail.com", "Today", false, "Santanda", "Password123#", "Active", "4456789002" },
                    { 26, 5, "email5@gmail.com", "Acada", false, "Spain", "Password123#", "Inactive", "4456789002" },
                    { 27, 5, "email6@gmail.com", "Lindi", true, "Phora", "Password123#", "Active", "4456789002" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminTable");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
