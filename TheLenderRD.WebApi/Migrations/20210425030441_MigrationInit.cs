using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLenderRD.WebApi.Migrations
{
    public partial class MigrationInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeRates",
                columns: table => new
                {
                    Age = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRates", x => x.Age);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Value = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    QuryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationDate = table.Column<DateTime>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AccountValue = table.Column<decimal>(nullable: false),
                    QueryIp = table.Column<string>(type: "char(15)", nullable: false),
                    MonthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.QuryId);
                    table.ForeignKey(
                        name: "FK_Logs_Months_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Months",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_MonthId",
                table: "Logs",
                column: "MonthId");

            migrationBuilder.Sql(@"

Create proc GetAgeRates
as
Begin

	Select * From AgeRates

End
GO

Create proc GetMonths
as
Begin

	Select * From Months

End
GO

Create proc InsertLog
@ConsultationDate Datetime,
@Edad int,
@Amount decimal,
@AccountValue decimal,
@QueryIp char(15),
@MonthId int
as
Begin

	Insert Into Logs values 
	(
		@ConsultationDate,
		@Edad,
		@Amount,
		@AccountValue,
		@QueryIp,
		@MonthId
	)

End
GO", true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgeRates");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Months");
        }
    }
}
