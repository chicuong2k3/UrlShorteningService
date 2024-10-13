using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShorteningService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fe8e234e-3fb1-4fa5-abc6-8e6aafc98a91"), 0, "7bacdbef-674f-4448-84c9-c302f45993ae", "test123@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEF+Eplnu5+4oK9yXcUklNHW9T0pypaU5qpmeq87RFiLrIYTpqdeDMxSWeSiOgCvRLQ==", null, false, null, false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fe8e234e-3fb1-4fa5-abc6-8e6aafc98a91"));
        }
    }
}
