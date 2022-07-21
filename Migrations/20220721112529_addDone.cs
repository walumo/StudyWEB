using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyWEB.Migrations
{
    public partial class addDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TopicIsDone",
                table: "Topic",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicIsDone",
                table: "Topic");
        }
    }
}
