using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.B20DCCN296.Core.Interfaces;

namespace MISA.B20DCCN296.API.Controllers
{
    [Route("api/v1/Departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentRepository _departmentsRepository;
        public DepartmentsController(IDepartmentRepository repository)
        {
            _departmentsRepository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _departmentsRepository.Get();
            return StatusCode(200, res);
        }
    }
}
