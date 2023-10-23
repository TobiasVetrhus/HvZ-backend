﻿using AutoMapper;
using HvZ_backend.Consts;
using HvZ_backend.Data.DTOs.Missions;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Missions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IMapper _mapper;

        public MissionController(IMissionService missionService, IMapper mapper)
        {
            _missionService = missionService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions()
        {
            var missions = await _missionService.GetAllAsync();
            var locationDTOs = _mapper.Map<IEnumerable<MissionDTO>>(missions);
            return Ok(locationDTOs);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MissionDTO>> GetMissionById(int id)
        {
            try
            {
                var mission = await _missionService.GetByIdAsync(id);
                return Ok(_mapper.Map<MissionDTO>(mission));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<MissionDTO>> AddMission(MissionPostDTO mission)
        {
            var newMission = await _missionService.AddAsync(_mapper.Map<Mission>(mission));

            return CreatedAtAction("GetMissionById", new { id = newMission.Id }, _mapper.Map<MissionDTO>(newMission));
        }


        [HttpPut("{id}/add-location/{locationId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AddMissionAsync(int id, int locationId)
        {
            try
            {
                await _missionService.AddLocationToMissionAsync(id, locationId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateMission(int id, MissionPutDTO mission)
        {
            if (id != mission.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedMission = await _missionService.UpdateAsync(_mapper.Map<Mission>(mission));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteMission(int id)
        {
            try
            {
                await _missionService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
