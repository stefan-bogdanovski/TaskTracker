using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Added_Initial_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CompletionDate", "CreatedAt", "DeletedAt", "ModifiedAt", "Name", "Priority", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "First project", 100, new DateTime(2021, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Second project", 200, new DateTime(2020, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Third project", 500, null, 0 },
                    { 4, new DateTime(2019, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Fourth project", 50, new DateTime(2018, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "Priority", "ProjectId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Create filters for projects, user should filter projects by date or by name.", null, "Create filters for projects", 1000, 1, 0 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Create sorting functionality for projects, user should sort projects by date", null, "Create sorting functionality for projects", 2000, 1, 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users should be able to contact administrator through form, when they fill the form, you should send the form details to chosen administrator.", null, "Implement mailing system", 3000, 2, 0 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "There are several bugs in our shopping cart", null, "Fix bugs in shopping cart", 500, 2, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
