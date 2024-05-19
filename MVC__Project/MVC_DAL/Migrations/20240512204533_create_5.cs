using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_DAL.Migrations
{
    /// <inheritdoc />
    public partial class create_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employes",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employes");
        }
    }
}
