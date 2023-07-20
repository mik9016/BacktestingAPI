using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BacktestAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInstrumentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntstrumentName",
                table: "Trades",
                newName: "InstrumentName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstrumentName",
                table: "Trades",
                newName: "IntstrumentName");
        }
    }
}
