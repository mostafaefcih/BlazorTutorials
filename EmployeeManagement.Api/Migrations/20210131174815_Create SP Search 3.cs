using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Api.Migrations
{
    public partial class CreateSPSearch3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var SPSearch = @"alter Procedure spSearchEmployees
                            @Name nvarchar(100) = NULL,
                            @Email nvarchar(50) = NULL,
                            @Gender int = NULL,
                            @DepartmentId int = NULL
                           As
                            Begin 
                                    Select * from Employees where
                                    (FirstName like '%'+@Name+'%' OR @Name IS NULL) OR
                                    (LastName like '%'+@Name+'%'  OR @Name  IS NULL) AND
                                    (DepartmentId      = @DepartmentId    OR @DepartmentId    IS NULL) AND
                                    (Gender      = @Gender    OR @Gender    IS NULL) AND
                                    (Email    like '%'+@Email+'%'     OR @Email    IS NULL)
                            End
                            Go";
            migrationBuilder.Sql(SPSearch);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string SPSearch = @"drop Procedure spSearchEmployees";
            migrationBuilder.Sql(SPSearch);

        }
    }
}
