using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.B20DCCN296.Core.DTOs;
using MISA.B20DCCN296.Core.Entities;
using MISA.B20DCCN296.Core.Interfaces;
using MISA.B20DCCN296.Core.Services;
using MISA.B20DCCN296.Infrastructure.Repositories;

namespace MISA.B20DCCN296.API.Controllers
{
    [Route("api/v1/Employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRespository _employeeRepository;
        IEmployeeService _employeeService;

        public EmployeeController(IEmployeeRespository respository, IEmployeeService employeeService)
        {
            _employeeRepository = respository;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _employeeRepository.Get();
            return StatusCode(200, res);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var res = _employeeRepository.Get(id);
            return StatusCode(200, res);
        }

        [HttpGet("CreteEmp/NewCode")]
        public IActionResult GetNewCode(int id)
        {
            var res = _employeeRepository.GetNewEmployeeCode();
            return StatusCode(200, res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var res = _employeeRepository.Delete(id);
                var message = new MessageResponse
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Xóa thành công"
                };

                return StatusCode(200, message);

            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMgs = "Có lỗi xảy ra",
                    devMsg = "",
                    error = ex.Message
                };
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert(Employee employee)
        {
            try
            {
                var result = _employeeService.InsertService(employee);
                if (result.Success == true)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(400, result);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMgs = "Có lỗi xảy ra",
                    devMsg = "",
                    error = ex.Message
                };

                return StatusCode(500, res);
            }
        }

        [HttpPost("Update")]
        public IActionResult Update(Employee employee)
        {
            try
            {
                var result = _employeeService.UpdateService(employee);
                if (result.Success == true)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(400, result);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMgs = "Có lỗi xảy ra",
                    devMsg = "",
                    error = ex.Message
                };

                return StatusCode(500, res);
            }
        }
    }
}
