using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepApiDemo.Model;
using WepApiDemo.Repositories.IRepositories;

namespace WepApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public BindController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        //public IActionResult Get1(int id,string name)// Binding primitive Type  => defult => Route OR Query
        //{
        //    return Ok();
        //}
        [HttpGet("{id:int}/{name:alpha}")]
        public IActionResult Get1(int id, string name)// Binding primitive Type  => defult => Route OR Query
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult add(Employee employee,string name)// Binding Complex Type  => defult => Body
        {
            return Ok();
        }
        [HttpPost("body/{id:int}")]
        public IActionResult post2([FromBody]string name,int id)
        {
            return Ok();
        }
       
    }
}
