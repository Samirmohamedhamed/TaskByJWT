using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepApiDemo.Dto;
using WepApiDemo.Model;
using WepApiDemo.Repositories.IRepositories;

namespace WepApiDemo.Controllers
{
    [Route("api/[controller]")]//Resourse URL : api/Employee
    [ApiController]
    [Authorize]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetEmployee()
        {
            var empolyees = employeeRepository.Get().ToList();
            return Ok(empolyees);
        }
        [HttpGet("{id:int}",Name ="EmployeeDetailsRoute")] //replace by Route
       // [Route("{id}")]//Concate with rote of controller
        public IActionResult GetEmployeeById([FromRoute]int id)
        {
            var empolyee = employeeRepository.GetFirstOrDefault(e=>e.Id==id);
            return Ok(empolyee);
        } 
        [HttpGet("{name:alpha}")] //replace by Route //Route api/Employee/name
       // [Route("{name}")]//Concate with rote of controller
        public IActionResult GetEmployeeById([FromRoute]string name )
        {
            var empolyee = employeeRepository.GetFirstOrDefault(e=>e.Name==name);
            return Ok(empolyee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute]int id,[FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                var oldemp = employeeRepository.GetFirstOrDefault(e => e.Id == id);
                if (oldemp != null)
                {
                    oldemp.Name = employee.Name;
                    oldemp.Address = employee.Address;
                    oldemp.Age = employee.Age;
                    oldemp.Salary = employee.Salary;
                    employeeRepository.Update(oldemp);
                    await employeeRepository.CommitAsync();
                    return StatusCode(204);

                }
            }
            return BadRequest(ModelState);


        }
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody]Employee employee)
        {
            if(ModelState.IsValid)
            {
             await  employeeRepository.CreateAsync(employee);
             await   employeeRepository.CommitAsync();
                var url = Url.Link("EmployeeDetailsRoute", new { id = employee.Id });
                //url in respone header //employee in respone body
                return Created(url,employee);

            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            var employee = employeeRepository.GetFirstOrDefault(e => e.Id == id);
             if(employee != null)
            {
                try
                {
                    employeeRepository.Delete(employee);
                    await employeeRepository.CommitAsync();
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
            }
            return BadRequest();

        }
        [HttpGet("ED/{id}")]
        public IActionResult GetEmployeeWithDepartment(int id)// Binding primitive Type  => defult => Route OR Query
        {
            Employee employee =  employeeRepository.GetFirstOrDefault(e => e.Id == id, Includes: [e => e.Department]);
            EmployeeWithDepartmentDto employeeWithDepartmentDto = new EmployeeWithDepartmentDto()
            {
                EmployeeId = employee.Id,
                DeptName = employee.Department.DeptName,
                EmpName = employee.Name,

            };
            return Ok(employeeWithDepartmentDto);
        }
    }
}
