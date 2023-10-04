namespace HvZ_backend.Data.DTOs.Missions
{
    public class MissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int GameId { get; set; }
    }
}
