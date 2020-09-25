using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CountryFriend.Repository.Migrations
{
    public partial class CreateDV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Filename = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Filename = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Surname = table.Column<string>(maxLength: 75, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 25, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(maxLength: 25, nullable: false),
                    State = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Filename = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    CountryName = table.Column<string>(maxLength: 25, nullable: false),
                    CountryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FriendFriend",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false),
                    Filename = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Surname = table.Column<string>(maxLength: 75, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 25, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(maxLength: 25, nullable: false),
                    State = table.Column<string>(maxLength: 25, nullable: false),
                    FriendName = table.Column<string>(maxLength: 50, nullable: false),
                    FriendId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendFriend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendFriend_Friend_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendFriend_FriendId",
                table: "FriendFriend",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendFriend");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
