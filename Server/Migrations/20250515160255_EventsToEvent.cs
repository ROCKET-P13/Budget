using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class EventsToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEvents",
                table: "CategoryEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetEvents",
                table: "BudgetEvents");

            migrationBuilder.RenameTable(
                name: "CategoryEvents",
                newName: "CategoryEvent");

            migrationBuilder.RenameTable(
                name: "BudgetEvents",
                newName: "BudgetEvent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEvent",
                table: "CategoryEvent",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetEvent",
                table: "BudgetEvent",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEvent",
                table: "CategoryEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetEvent",
                table: "BudgetEvent");

            migrationBuilder.RenameTable(
                name: "CategoryEvent",
                newName: "CategoryEvents");

            migrationBuilder.RenameTable(
                name: "BudgetEvent",
                newName: "BudgetEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEvents",
                table: "CategoryEvents",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetEvents",
                table: "BudgetEvents",
                column: "id");
        }
    }
}
