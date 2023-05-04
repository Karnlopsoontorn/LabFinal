using LabFinal.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LabFinal.Models;
using Microsoft.EntityFrameworkCore;




namespace LabFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Employee : ControllerBase
    {

        //Variable
        private readonly DataDbcontext _dbcontext;

        public Employee (DataDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<employees>>> getEmployees()
        {
            var Employee = await _dbcontext.employees.ToListAsync();

            if (Employee.Count == 0)
            {
                return NotFound();
            }

            return Ok(Employee);
        }

        [HttpGet("ID")]
        public async Task<ActionResult<List<employees>>> getEmployeeById(int ID)
        {
            var Employee = await _dbcontext.employees.FindAsync(ID);

            if (Employee == null)
            {
                return NotFound();
            }

            return Ok(Employee);
        }

        [HttpGet("Salary")]
        public async Task<ActionResult<List<employees>>> getEmployeeSalary(String ID)
        {
            var Employee = await _dbcontext.employees.FindAsync(ID);

            if (Employee == null)
            {
                return NotFound();
            }


            var yearsOfWork = (DateTime.Now.Year - Employee.hireDate.Year) - 1;

            var position = _dbcontext.positions.Find(Employee.position_Id);

            if (position == null)
            {
                return NotFound();
            }
            var salary = (position.baseSalary + (position.baseSalary * position.salaryIncreaseRate)) * yearsOfWork;

            return Ok(salary);
            }

            [HttpPost]
            public async Task<ActionResult<employees>> createEmployees(employees employees)
            {

                try
                {
                    var position = _dbcontext.positions.FirstOrDefault(p => p.position_Id == employees.position_Id);
                    if (position == null)
                    {
                        return BadRequest("Invalid position ID");
                    }

                    _dbcontext.employees.Add(employees);
                    _dbcontext.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return BadRequest();
                }

                return Ok(employees);
            }

        [HttpPut]
        public async Task<ActionResult<employees>> updateEmployees(string id, employees newEmployees)
        {
            try
            {
                if (_dbcontext.positions.FirstOrDefault(p => p.position_Id == newEmployees.position_Id) == null)
                {
                    return BadRequest("Invalid position ID");
                }

                var employees = await _dbcontext.employees.FindAsync(id);
                if (employees == null)
                {
                    return NotFound();
                }


                employees.empName = newEmployees.empName;
                employees.email = newEmployees.email;
                employees.phoneNumber = newEmployees.phoneNumber;
                employees.hireDate = newEmployees.hireDate;
                employees.position_Id = newEmployees.position_Id;

                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return Ok(newEmployees);
        }


        [HttpDelete]
        public async Task<ActionResult<employees>> deleteEmployees(string id)
        {
            var employees = await _dbcontext.employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            //Remove
            _dbcontext.employees.Remove(employees);

            //save
            await _dbcontext.SaveChangesAsync();

            return Ok(employees);
        }
    }
}
