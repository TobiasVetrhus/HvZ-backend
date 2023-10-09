using System.Collections.Generic;
using System.Threading.Tasks;
using HvZ_backend.Data.DTOs.Rules;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Rules
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> GetAllAsync();
        Task<Rule> GetByIdAsync(int id);
        Task<Rule> AddAsync(Rule rule);
        Task<Rule> UpdateAsync(Rule rule);
        Task DeleteByIdAsync(int id);
    }
}
