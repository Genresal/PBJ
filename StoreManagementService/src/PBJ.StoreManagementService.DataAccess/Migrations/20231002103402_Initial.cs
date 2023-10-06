using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PBJ.StoreManagementService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    UserEmail = table.Column<string>(type: "nchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nchar(50)", nullable: false),
                    FollowerEmail = table.Column<string>(type: "nchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollowers_Users_FollowerEmail",
                        column: x => x.FollowerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_UserFollowers_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    UserEmail = table.Column<string>(type: "nchar(50)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email" },
                values: new object[,]
                {
                    { 1, "unique1@email.com" },
                    { 2, "unique2@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreatedAt", "UserEmail" },
                values: new object[,]
                {
                    { 1, "PostContent1", new DateTime(2023, 10, 2, 12, 34, 2, 146, DateTimeKind.Local).AddTicks(390), "unique1@email.com" },
                    { 2, "PostContent2", new DateTime(2023, 10, 2, 12, 34, 2, 146, DateTimeKind.Local).AddTicks(411), "unique1@email.com" }
                });

            migrationBuilder.InsertData(
                table: "UserFollowers",
                columns: new[] { "Id", "FollowerEmail", "UserEmail" },
                values: new object[,]
                {
                    { 1, "unique2@email.com", "unique1@email.com" },
                    { 2, "unique1@email.com", "unique2@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "PostId", "UserEmail" },
                values: new object[,]
                {
                    { 1, "CommentContent1", new DateTime(2023, 10, 2, 12, 34, 2, 145, DateTimeKind.Local).AddTicks(3835), 2, "unique1@email.com" },
                    { 2, "CommentContent2", new DateTime(2023, 10, 2, 12, 34, 2, 145, DateTimeKind.Local).AddTicks(3880), 1, "unique2@email.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserEmail",
                table: "Comments",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserEmail",
                table: "Posts",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowerEmail",
                table: "UserFollowers",
                column: "FollowerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_UserEmail",
                table: "UserFollowers",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "UserFollowers");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
