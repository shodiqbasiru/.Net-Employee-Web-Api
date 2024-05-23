using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialEmployeeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "NVarchar(50)", nullable: false),
                    email = table.Column<string>(type: "NVarchar(50)", nullable: true),
                    password = table.Column<string>(type: "NVarchar(50)", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_group",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    groupname = table.Column<string>(name: "group_name", type: "NVarchar(50)", nullable: false),
                    groupdescription = table.Column<string>(name: "group_description", type: "NVarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employeename = table.Column<string>(name: "employee_name", type: "NVarchar(50)", nullable: false),
                    address = table.Column<string>(type: "NVarchar(250)", nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "NVarchar(14)", nullable: true),
                    birthdate = table.Column<DateTime>(name: "birth_date", type: "datetime2", nullable: false),
                    basicsalary = table.Column<double>(name: "basic_salary", type: "float", nullable: false),
                    groupid = table.Column<Guid>(name: "group_id", type: "uniqueidentifier", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_employee_m_account_account_id",
                        column: x => x.accountid,
                        principalTable: "m_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_employee_m_group_group_id",
                        column: x => x.groupid,
                        principalTable: "m_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_employee_account_id",
                table: "m_employee",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_employee_group_id",
                table: "m_employee",
                column: "group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_employee");

            migrationBuilder.DropTable(
                name: "m_account");

            migrationBuilder.DropTable(
                name: "m_group");
        }
    }
}
