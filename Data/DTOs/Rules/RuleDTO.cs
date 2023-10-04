namespace HvZ_backend.Data.DTOs.Rules
{
    public class RuleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[]? GameIds { get; set; }
    }
}
