using AutoMapper;
using HvZ_backend.Data.DTOs.Rules;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Rules;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Get a list of all rules with GameIds.
        /// </summary>

        [HttpGet("GetRules")]
        public async Task<ActionResult<IEnumerable<RuleDTO>>> GetRules()
        {
            var rules = await _ruleService.GetAllAsync();
            var ruleDTOs = _mapper.Map<IEnumerable<RuleDTO>>(rules);
            return Ok(ruleDTOs);
        }

        // GET: api/v1/Rules/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleDTO>> GetRule(int id)
        {
            try
            {
                // Retrieve a rule by ID and return it
                return Ok(_mapper.Map<RuleDTO>(await _ruleService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                // Handle the case where the rule with the specified ID was not found
                return NotFound(ex.Message);
            }
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
        // POST: api/v1/Rules
        [HttpPost]
        public async Task<ActionResult<RuleDTO>> PostRule(RulePostDTO rule)
        {
            // Create a new rule and return CreatedAtAction with the newly created rule's information
            var newRule = await _ruleService.AddAsync(_mapper.Map<Rule>(rule));

            return CreatedAtAction("GetRule",
                new { id = newRule.Id },
                _mapper.Map<RuleDTO>(newRule));
        }

        // DELETE: api/v1/Rules/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            try
            {
                // Delete a rule by ID and return NoContent on success
                await _ruleService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                // Handle the case where the rule with the specified ID was not found
                return NotFound(ex.Message);
            }
        }
    }
}
