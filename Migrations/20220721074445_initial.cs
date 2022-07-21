using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyWEB.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Topic_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicTitle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TopicDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TopicEstimatedTimeToMaster = table.Column<double>(type: "float", nullable: false),
                    TopicTimeSpent = table.Column<double>(type: "float", nullable: true),
                    TopicSource = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TopicStartLearningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TopicInProgress = table.Column<bool>(type: "bit", nullable: false),
                    TopicCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Topic_ID);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Task_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTitle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TaskDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TaskPriority = table.Column<int>(type: "int", nullable: true),
                    TaskDeadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    TaskDone = table.Column<bool>(type: "bit", nullable: true),
                    Topic_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Task_Id);
                    table.ForeignKey(
                        name: "FK_Task_Topic",
                        column: x => x.Topic_Id,
                        principalTable: "Topic",
                        principalColumn: "Topic_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Task_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Task",
                        column: x => x.Task_Id,
                        principalTable: "Task",
                        principalColumn: "Task_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_Task_Id",
                table: "Note",
                column: "Task_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Topic_Id",
                table: "Task",
                column: "Topic_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Topic");
        }
    }
}
