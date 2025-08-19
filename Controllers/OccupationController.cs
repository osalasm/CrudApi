using CrudSimple.Context;
using CrudSimple.DTOs;
using CrudSimple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OccupationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("occupations")]
        public async Task<ActionResult<List<OccupationDTO>>> Get()
        {
            try
            {
                List<OccupationDTO> dtoList = new List<OccupationDTO>();

                List<Occupation> occupationList = await _context.Occupations.ToListAsync();

                foreach (Occupation item in occupationList)
                {
                    dtoList.Add(new OccupationDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }

                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
