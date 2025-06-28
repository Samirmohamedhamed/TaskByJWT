using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepApiDemo.Dto;
using WepApiDemo.Model;
using WepApiDemo.Repositories.IRepositories;

namespace WepApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var departments = departmentRepository.Get();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            Department department = departmentRepository.GetFirstOrDefault(x => x.Id == id, [e => e.Employees]);
            DepartmentWithEmployees departmentWithEmployees = new DepartmentWithEmployees()
            {
                Id = department.Id,
                Name = department.DeptName,
            };
            foreach (var item in department.Employees)
            {
                departmentWithEmployees.EmplyoeeDtos.Add(new EmplyoeeDto { Id = item.Id, Name = item.Name });
            }
            return Ok(departmentWithEmployees);
        }
    }

}

