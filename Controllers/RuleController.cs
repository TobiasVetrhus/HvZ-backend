﻿using AutoMapper;
using HvZ_backend.Data.DTOs.Rules;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Rules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        private readonly IMapper _mapper;

        public RulesController(IRuleService ruleService, IMapper mapper)
        {
            _ruleService = ruleService;
            _mapper = mapper;
        }

        // GET: Api/v1/Rules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleDTO>>> GetRules()
        {
            // Retrieve and return a list of rules
            return Ok(_mapper.Map<IEnumerable<RuleDTO>>(await _ruleService.GetAllAsync()));
        }
        
        
        // PUT: api/v1/Rules/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(int id, RulePutDTO rule)
        {
            if (id != rule.Id)
            {
                // If the provided ID in the URL does not match the ID in the request body, return BadRequest
                return BadRequest();
            }
            try
            {
                // Update the rule and return NoContent on success
                var updatedRule = await _ruleService.UpdateAsync(_mapper.Map<Rule>(rule));
            }
            catch (EntityNotFoundException ex)
            {
                // Handle the case where the rule with the specified ID was not found
                return NotFound(ex.Message);
            }

            return NoContent();
        }

    }
}
