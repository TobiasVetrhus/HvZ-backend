﻿using HvZ_backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Games
{
    public class GamePutDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public GameStatus GameState { get; set; }
        [StringLength(50)]
        public string PictureURL { get; set; }
        public int[]? RuleIds { get; set; }
        public int[]? PlayerIds { get; set; }
        public int[]? MissionIds { get; set; }
        public int[]? ConversationIds { get; set; }
    }
}
