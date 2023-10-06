using AutoMapper;
using HvZ_backend.Data.DTOs.Locations;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Locations;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            var locations = await _locationService.GetAllAsync();
            var locationDTOs = _mapper.Map<IEnumerable<LocationDTO>>(locations);
            return Ok(locationDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocationById(int id)
        {
            try
            {
                var location = await _locationService.GetByIdAsync(id);
                return Ok(_mapper.Map<LocationDTO>(location));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
