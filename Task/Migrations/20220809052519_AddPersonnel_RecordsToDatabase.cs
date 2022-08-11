using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Migrations
{
    public partial class AddPersonnel_RecordsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personnel_Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payroll_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forenames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surenames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_of_Birth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMail_Home = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start_Date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel_Records", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personnel_Records");
        }
    }
}
