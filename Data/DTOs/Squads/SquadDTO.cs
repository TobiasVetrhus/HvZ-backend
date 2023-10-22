namespace HvZ_backend.Data.DTOs.Squads
{
    public class SquadDTO
    {
        public int Id { get; set; }
        public string SquadName { get; set; }
        public int? NumberOfMembers { get; set; }
        public int? NumberOfDeceased { get; set; }
        public int? GameId { get; set; }
        public int[]? PlayerIds { get; set; }
    }
}
