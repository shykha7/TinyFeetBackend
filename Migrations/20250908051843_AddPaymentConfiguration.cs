using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyFeetBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payments",
                newName: "PaidAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaidAt",
                table: "Payments",
                newName: "PaymentDate");
        }
    }
}
