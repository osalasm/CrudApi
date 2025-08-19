using CrudSimple.Context;
using CrudSimple.DTOs;
using CrudSimple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetList()
        {
            try
            {
                List<EmployeeDTO> dtoList = new List<EmployeeDTO>();

                List<Employee> employeesList = await _context.Employees.Include(o => o.Occupations).ToListAsync();

                foreach (Employee item in employeesList)
                {
                    dtoList.Add(new EmployeeDTO
                    {
                        Id = item.Id,
                        FullName = item.FullName,
                        Salary = item.Salary,
                        IdOccupation = item.IdOccupation,
                        OccupationName = item.Occupations.Name,
                    });
                }

                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("employees/{id}")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetById(int id)
        {
            try
            {
                EmployeeDTO employeeDTO = new EmployeeDTO();

                Employee employee = await _context.Employees.Include(o => o.Occupations).FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null) return NotFound("Employee not found");

                employeeDTO.Id = id;
                employeeDTO.FullName = employee.FullName;
                employeeDTO.Salary = employee.Salary;
                employeeDTO.IdOccupation = employee.IdOccupation;
                employeeDTO.OccupationName = employee.Occupations.Name;

                return Ok(employeeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("employees")]
        public async Task<ActionResult<List<EmployeeDTO>>> Save(EmployeeDTO dto)
        {
            try
            {
                Employee employee = new Employee
                {
                    IdOccupation = dto.IdOccupation,
                    FullName = dto.FullName,
                    Salary = dto.Salary
                };

                await _context.AddAsync(employee);
                await _context.SaveChangesAsync();

                return Ok("Saved successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("employees")]
        public async Task<ActionResult<List<EmployeeDTO>>> Edit(EmployeeDTO dto)
        {
            try
            {
                Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (employee == null) return NotFound("Employee not found");

                employee.FullName = dto.FullName;
                employee.Salary = dto.Salary;
                employee.IdOccupation = dto.IdOccupation;

                await _context.SaveChangesAsync();

                return Ok("Employee modified correctly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("employees/{id}")]
        public async Task<ActionResult<List<EmployeeDTO>>> Delete(int id)
        {
            try
            {
                Employee employee = await _context.Employees.FindAsync(id);

                if (employee == null) return NotFound("Employee not found");

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok("Employee deleted correctly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
