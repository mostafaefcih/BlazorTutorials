using AspNetCore.Reporting;
using EmployeeManagement.Api.Helpers;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Models.Filter;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Filter;
using EmployeeManagement.Models.Sort;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUriService uriService;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHostEnvironment,

            IUriService uriService)
        {
            this.employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            this.uriService = uriService;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        [HttpGet("GenerateReport")]
        public async Task<IActionResult> GenerateReport() {

            var dt = new DataTable();
            dt.Columns.Add("FullName");
            dt.Columns.Add("Email");
            dt.Columns.Add("DateOfBirth");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Department");
            var employeeList = await employeeRepository.GetEmployees();
            DataRow dr;
            foreach (var emp in employeeList)
            {
                dr = dt.NewRow();
                dr["FullName"] = $"{emp.FirstName}{emp.LastName} ";
                dr["Email"] = emp.Email;
                dr["Gender"] = emp.Gender;
                dr["Department"] = emp.Department.DepartmentName;
                dr["DateOfBirth"] = emp.DateOfBrith;
                dt.Rows.Add(dr);
            }
            string mimeType = "";
            int extenstion = 1;

            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\EmployeeReport.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("param", "Blazor RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployee", dt);
            var result = localReport.Execute(RenderType.Pdf,extenstion,parameters,mimeType);
            return File(result.MainStream,"application/pdf");
        
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter ,[FromQuery] SortCriteria criteria)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
             var pagedData = await employeeRepository.GetAll(validFilter,criteria);
            var pagedReponse = PaginationHelper.CreatePagedReponse<Employee>(pagedData, validFilter, pagedData.TotalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpPost("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(EmployeeFilter filter)
        {
            try
            {
                var result = await employeeRepository.Search(filter);

                //if (result.Any())
                //{
                    return Ok(result);
                //}

                //return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId },
                    createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                

                var employeeToUpdate = await employeeRepository.GetEmployee(employee.EmployeeId);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {employee.EmployeeId} not found");
                }

                return await employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployee(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}