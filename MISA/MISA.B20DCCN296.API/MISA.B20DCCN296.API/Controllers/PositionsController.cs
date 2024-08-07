using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.B20DCCN296.Core.Interfaces;

namespace MISA.B20DCCN296.API.Controllers
{
    [Route("api/v1/Positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        IPositionRepository _positionRepository;
        public PositionsController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpGet]
        public IActionResult Get() 
        { 
            var res = _positionRepository.Get();
            return StatusCode(200, res);
        }
    }
}
