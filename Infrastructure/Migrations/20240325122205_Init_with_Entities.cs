using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_with_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessFaildCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Banned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WasRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Epics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epics_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_To_Projects",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_To_Projects", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_Users_To_Projects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_To_Projects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Importance = table.Column<byte>(type: "tinyint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproximateDateOfCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusTask = table.Column<int>(type: "int", nullable: false),
                    PreviousTaskId = table.Column<long>(type: "bigint", nullable: true),
                    EpicId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Epics_EpicId",
                        column: x => x.EpicId,
                        principalTable: "Epics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Tasks_Tasks_PreviousTaskId",
                        column: x => x.PreviousTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTaskId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Tasks_WorkTaskId",
                        column: x => x.WorkTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTaskId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Tasks_TaskId",
                        column: x => x.WorkTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UserId",
                table: "Attachment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_WorkTaskId",
                table: "Attachment",
                column: "WorkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TaskId",
                table: "Comments",
                column: "WorkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Epics_ProjectId",
                table: "Epics",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UserId",
                table: "Note",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifies_UserId",
                table: "Notifies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorId",
                table: "Projects",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EpicId",
                table: "Tasks",
                column: "EpicId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PreviousTaskId",
                table: "Tasks",
                column: "PreviousTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_To_Projects_ProjectId",
                table: "Users_To_Projects",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Notifies");

            migrationBuilder.DropTable(
                name: "Users_To_Projects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Epics");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
