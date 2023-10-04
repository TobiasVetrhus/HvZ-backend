namespace HvZ_backend.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Exception thrown for a specific entity not found in the database. 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        public EntityNotFoundException(string entityType, object entityId)
            : base($"Entity '{entityType}' with ID '{entityId}' not found.")
        {
        }
    }
}
