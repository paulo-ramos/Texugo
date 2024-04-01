using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Texugo.Login.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 120, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "VARCHAR", maxLength: 8, nullable: false),
                    EmailVerificationExpireAt = table.Column<DateTime>(type: "DATE", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<DateTime>(type: "DATE", nullable: true),
                    PasswordHash = table.Column<string>(type: "VARCHAR", maxLength: 120, nullable: false),
                    PasswordResetCode = table.Column<string>(type: "VARCHAR", maxLength: 8, nullable: false),
                    Image = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
