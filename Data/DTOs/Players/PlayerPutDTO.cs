﻿using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Player
{
    public class PlayerPutDTO
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string Username { get; set; }

        public bool Zombie { get; set; }

        [StringLength(8)]
        public string BiteCode { get; set; }
        public Guid UserId { get; set; }
        public int LocationId { get; set; }
        public int SquadId { get; set; }
        public int GameId { get; set; }
    }
}