using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_DAL.Migrations
{
    /// <inheritdoc />
    public partial class imagecolumnb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employes");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Employes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employes",
                type: "bit",
                nullable: true);
        }
    }
}
