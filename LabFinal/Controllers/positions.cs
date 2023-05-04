
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabFinal.Database;
using LabFinal.Models;
using Microsoft.AspNetCore.Http;


namespace LabFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class positions : ControllerBase
    {
        private readonly DataDbcontext _dbcontext;



        //Cotructure Method
        public positions(DataDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<positions>>> getPositions()
        {
            var positions = await _dbcontext.positions.ToListAsync();

            if (positions.Count == 0)
            {
                return NotFound();
            }
            return Ok(positions);
        }


        //Get Id
        [HttpGet("id")]
        public async Task<ActionResult<positions>> getPositionById(string id)
        {
            var positions = await _dbcontext.positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }

        [HttpGet("Positions")]
        public async Task<ActionResult<positions>> getEmpByPositionId(string id)
        {
            var position = _dbcontext.employees.FirstOrDefault(e => e.empId == id);
            if (position == null)
            {
                return NotFound();
            }
            return Ok(position);
        }


        [HttpPost]
        
        public async Task<ActionResult<positions>> PostPosition(Positions positions)
        {
            try
            {
                _dbcontext.positions.Add(positions);
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

                return Ok(positions);
        }

        [HttpPut]
        public async Task<ActionResult<positions>> putPosition(int id, Positions positions)
        {
            var Positions = await _dbcontext.positions.FindAsync(id);
            
            if (positions == null)
            {
                return NotFound();
            }

            positions.position_Id = Positions.position_Id;
            positions.position_Name = Positions.position_Name;
            positions.baseSalary = Positions.baseSalary;
            positions.salaryIncreaseRate = Positions.salaryIncreaseRate;

            await _dbcontext.SaveChangesAsync();
            return Ok(positions);
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<positions>> deletePositions(string id)
        {
            var employees = _dbcontext.employees.Where(e => e.position_Id == id).ToList();
            if (employees != null && employees.Count > 0)
            {
                return BadRequest("Cannot delete position ");
            }

            var position = _dbcontext.positions.SingleOrDefault(p => p.position_Id == id);
            if (position == null)
            {
                return NotFound();
            }

            _dbcontext.positions.Remove(position);
            _dbcontext.SaveChanges();
            return Ok();
        }

    }

}
