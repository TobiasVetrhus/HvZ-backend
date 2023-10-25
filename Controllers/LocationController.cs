using AutoMapper;
using HvZ_backend.Data.DTOs.Locations;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Locations;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve a list of all locations.
        /// </summary>
        /// <returns>An action result containing a list of LocationDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            var locations = await _locationService.GetAllAsync();
            var locationDTOs = _mapper.Map<IEnumerable<LocationDTO>>(locations);
            return Ok(locationDTOs);
        }

        /// <summary>
        /// Retrieve a specific location by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the location.</param>
        /// <returns>An action result containing a LocationDTO representing the requested location.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested location is not found.</exception>
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

        /// <summary>
        /// Add a new location to the system.
        /// </summary>
        /// <param name="location">A DTO representing the location to be added.</param>
        /// <returns>An action result containing a LocationDTO for the newly added location.</returns>
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> AddLocation(LocationPostDTO location)
        {
            var newLocation = await _locationService.AddAsync(_mapper.Map<Location>(location));

            return CreatedAtAction("GetLocationById", new { id = newLocation.Id }, _mapper.Map<LocationDTO>(newLocation));
        }

        /// <summary>
        /// Update an existing location with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the location to update.</param>
        /// <param name="location">A DTO representing the updated location information.</param>
        /// <returns>An action result indicating success or failure of the update.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested location is not found.</exception>
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

        /// <summary>
        /// Delete a location by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the location to delete.</param>
        /// <returns>An action result indicating success or failure of the deletion.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested location is not found.</exception>
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
