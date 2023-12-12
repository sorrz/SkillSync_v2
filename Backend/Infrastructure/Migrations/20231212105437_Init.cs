using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactMail = table.Column<string>(type: "TEXT", nullable: false),
                    ContactPhone = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    TechStack = table.Column<string>(type: "TEXT", nullable: false),
                    Mentorship = table.Column<bool>(type: "INTEGER", nullable: false),
                    Lia1Spots = table.Column<int>(type: "INTEGER", nullable: false),
                    Lia2Spots = table.Column<int>(type: "INTEGER", nullable: false),
                    HasExjob = table.Column<bool>(type: "INTEGER", nullable: false),
                    Presentation = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    StudentSalt = table.Column<string>(type: "TEXT", nullable: false),
                    TechStack = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Graduation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartLia1 = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndLia1 = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartLia2 = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndLia2 = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Presentation = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ConnectedTo = table.Column<string>(type: "TEXT", nullable: true),
                    LinkedInProfile = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
