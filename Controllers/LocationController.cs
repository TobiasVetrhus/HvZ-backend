using AutoMapper;
using HvZ_backend.Data.DTOs.Locations;
using HvZ_backend.Data.Entities;
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

        [HttpPost]
        public async Task<ActionResult<LocationDTO>> AddLocation(LocationPostDTO location)
        {
            var newLocation = await _locationService.AddAsync(_mapper.Map<Location>(location));

            return CreatedAtAction("GetLocationById", new { id = newLocation.Id }, _mapper.Map<LocationDTO>(newLocation));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationPutDTO location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedLocation = await _locationService.UpdateAsync(_mapper.Map<Location>(location));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                await _locationService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
