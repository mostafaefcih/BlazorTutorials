﻿ using EmployeeManagement.Api.Constants;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentRepository departmentRepository;

    public DepartmentsController(IDepartmentRepository departmentRepository)
    {
        this.departmentRepository = departmentRepository;
    }
    //[Authorize(Roles = Roles.Admin.ToString())]
    //[Authorize(Roles ="Admin")]
    //[HttpGet("GetAll")]
    public async Task<ActionResult> GetDepartments()
    {

        try
        {
            return Ok(await departmentRepository.GetDepartments());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        try
        {
            var result = await departmentRepository.GetDepartment(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
}