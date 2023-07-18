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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nchar(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Login = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Following",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowingDate = table.Column<DateTime>(type: "date", nullable: false),
                    FollowingUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Following", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Following_User_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nchar(255)", maxLength: 255, nullable: false),
                    PostDate = table.Column<DateTime>(type: "date", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionDate = table.Column<DateTime>(type: "date", nullable: false),
                    SubscribedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_User_SubscribedUserId",
                        column: x => x.SubscribedUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FollowingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollowing_Following_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "Following",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollowing_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: false),
                    CommentDate = table.Column<DateTime>(type: "date", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscription_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSubscription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "LastName", "Login", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 18, 15, 35, 47, 841, DateTimeKind.Local).AddTicks(3176), "Lastname1", "login1", "Name1", "Surname1" },
                    { 2, new DateTime(2023, 7, 18, 15, 35, 47, 841, DateTimeKind.Local).AddTicks(3185), "Lastname2", "login2", "Name2", "Surname2" }
                });

            migrationBuilder.InsertData(
                table: "Following",
                columns: new[] { "Id", "FollowingDate", "FollowingUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 18, 15, 35, 47, 840, DateTimeKind.Local).AddTicks(1924), 1 },
                    { 2, new DateTime(2023, 7, 18, 15, 35, 47, 840, DateTimeKind.Local).AddTicks(1928), 2 }
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "Id", "Content", "PostDate", "UserId" },
                values: new object[,]
                {
                    { 1, "PostContent1", new DateTime(2023, 7, 18, 15, 35, 47, 840, DateTimeKind.Local).AddTicks(6035), 1 },
                    { 2, "PostContent2", new DateTime(2023, 7, 18, 15, 35, 47, 840, DateTimeKind.Local).AddTicks(6042), 2 }
                });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "SubscribedUserId", "SubscriptionDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 18, 15, 35, 47, 840, DateTimeKind.Local).AddTicks(8485) },
                    { 2, 2, new DateTime(2023, 7, 18, 15, 35, 47, 840, DateTimeKind.Local).AddTicks(8490) }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "CommentDate", "Content", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 18, 15, 35, 47, 839, DateTimeKind.Local).AddTicks(9223), "CommentContent1", 2, 1 },
                    { 2, new DateTime(2023, 7, 18, 15, 35, 47, 839, DateTimeKind.Local).AddTicks(9237), "CommentContent2", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserFollowing",
                columns: new[] { "Id", "FollowingId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserSubscription",
                columns: new[] { "Id", "SubscriptionId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Following_FollowingUserId",
                table: "Following",
                column: "FollowingUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubscribedUserId",
                table: "Subscription",
                column: "SubscribedUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowing_FollowingId",
                table: "UserFollowing",
                column: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_SubscriptionId",
                table: "UserSubscription",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_UserId",
                table: "UserSubscription",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "UserFollowing");

            migrationBuilder.DropTable(
                name: "UserSubscription");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Following");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
