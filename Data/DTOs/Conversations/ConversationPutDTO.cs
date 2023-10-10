﻿using HvZ_backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Conversations
{
    public class ConversationPutDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ConversationName { get; set; }
        public ChatType ChatType { get; set; }
    }
}
