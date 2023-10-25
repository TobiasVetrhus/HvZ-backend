using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Rules
{
    /// <summary>
    /// Interface for managing HvZ game rules, including CRUD operations (Create, Read, Update, Delete).
    /// </summary>
    public interface IRuleService
    {
        /// <summary>
        /// Retrieve all HvZ game rules from the database.
        /// </summary>
        Task<IEnumerable<Rule>> GetAllAsync();

        /// <summary>
        /// Retrieve a specific HvZ game rule by its ID.
        /// </summary>
        /// <param name="id">The ID of the rule to retrieve.</param>
        Task<Rule> GetByIdAsync(int id);

        /// <summary>
        /// Add a new HvZ game rule to the database.
        /// </summary>
        /// <param name="rule">The rule to add.</param>
        Task<Rule> AddAsync(Rule rule);

        /// <summary>
        /// Update an existing HvZ game rule in the database.
        /// </summary>
        /// <param name="rule">The updated rule information.</param>
        Task<Rule> UpdateAsync(Rule rule);

        /// <summary>
        /// Delete an HvZ game rule by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the rule to delete.</param>
        Task DeleteByIdAsync(int id);
    }
}
