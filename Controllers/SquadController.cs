﻿using AutoMapper;
using HvZ_backend.Data.DTOs.Player;
using HvZ_backend.Data.DTOs.Squads;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Squads;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HvZ_backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SquadController : ControllerBase
    {
        private readonly ISquadService _squadService;
        private readonly IMapper _mapper;

        public SquadController(ISquadService squadService, IMapper mapper)
        {
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get a list of all squads.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquads()
        {
            var squads = await _squadService.GetAllAsync();
            var squadDTOs = _mapper.Map<IEnumerable<SquadDTO>>(squads);
            return Ok(squadDTOs);
        }
        /// <summary>
        /// Get a squad by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadDTO>> GetSquadById(int id)
        {
            try
            {
                var squad = await _squadService.GetByIdAsync(id);
                return Ok(_mapper.Map<SquadDTO>(squad));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

