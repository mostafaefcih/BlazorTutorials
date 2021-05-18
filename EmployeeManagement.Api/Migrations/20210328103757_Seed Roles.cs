using System;
using EmployeeManagement.Api.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Api.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "AspNetRoles",
                columns: new string[] {
               "Id","Name","NormalizedName","ConcurrencyStamp"
                },
                values: new string[] {Guid.NewGuid().ToString(), Roles.Admin.ToString(), Roles.Admin.ToString().ToUpper(), Guid.NewGuid().ToString() });
            migrationBuilder.InsertData(table: "AspNetRoles",
                  columns: new string[] {
               "Id","Name","NormalizedName","ConcurrencyStamp"
                  },
                  values: new string[] { Guid.NewGuid().ToString(), Roles.User.ToString(), Roles.User.ToString().ToUpper(), Guid.NewGuid().ToString() });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AspNetRoles ");
        }
    }
}
