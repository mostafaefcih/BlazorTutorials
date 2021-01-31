using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Api.Migrations
{
    public partial class CreateSPSearch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var SPSearch = @"create Procedure spSearchEmployees
                            @FirstName nvarchar(100) = NULL,
                            @LastName nvarchar(100) = NULL,
                            @Email nvarchar(50) = NULL,
                            @Gender int = NULL,
                            @DepartmentId int = NULL
                           As
                            Begin 
                                    Select * from Employees where
                                    (FirstName = @FirstName OR @FirstName IS NULL) AND
                                    (LastName  = @LastName  OR @LastName  IS NULL) AND
                                    (DepartmentId      = @DepartmentId    OR @DepartmentId    IS NULL) AND
                                    (Gender      = @Gender    OR @Gender    IS NULL) AND
                                    (Email      = @Email    OR @Email    IS NULL)
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
