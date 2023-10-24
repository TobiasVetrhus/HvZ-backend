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

        /// <summary>
        /// Get a rule by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the rule to retrieve.</param>
        /// <returns>An action result containing a RuleDTO representing the requested rule.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested rule is not found.</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleDTO>> GetRule(int id)
        {
            try
            {
                return Ok(_mapper.Map<RuleDTO>(await _ruleService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Update an existing rule by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the rule to update.</param>
        /// <param name="rule">A DTO representing the updated rule information.</param>
        /// <returns>An action result indicating success or failure of the update.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested rule is not found.</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(int id, RulePutDTO rule)
        {
            if (id != rule.Id)
            {
                return BadRequest();
            }
            try
            {
                var updatedRule = await _ruleService.UpdateAsync(_mapper.Map<Rule>(rule));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Create a new rule.
        /// </summary>
        /// <param name="rule">A DTO representing the rule to be created.</param>
        /// <returns>An action result containing a RuleDTO for the newly created rule.</returns>
        [HttpPost]
        public async Task<ActionResult<RuleDTO>> PostRule(RulePostDTO rule)
        {
            var newRule = await _ruleService.AddAsync(_mapper.Map<Rule>(rule));

            return CreatedAtAction("GetRule",
                new { id = newRule.Id },
                _mapper.Map<RuleDTO>(newRule));
        }

        /// <summary>
        /// Delete a rule by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the rule to delete.</param>
        /// <returns>An action result indicating success or failure of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            try
            {
                await _ruleService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
