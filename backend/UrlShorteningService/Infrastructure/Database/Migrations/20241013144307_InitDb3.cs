using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShorteningService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fe8e234e-3fb1-4fa5-abc6-8e6aafc98a91"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "471df35e-7666-44fb-8378-0fc36c67eb20", "TEST123@GMAIL.COM", "TEST123@GMAIL.COM", "AQAAAAIAAYagAAAAEEby9nxkQL8mr013T47PjdlzKJabD67HZWiXzvQZA0KjT51XufummFH2j2UWevG6MQ==", "test123@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fe8e234e-3fb1-4fa5-abc6-8e6aafc98a91"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "7bacdbef-674f-4448-84c9-c302f45993ae", null, null, "AQAAAAIAAYagAAAAEF+Eplnu5+4oK9yXcUklNHW9T0pypaU5qpmeq87RFiLrIYTpqdeDMxSWeSiOgCvRLQ==", null });
        }
    }
}
