using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Leaderboard
    {
        [Key]
        public Guid User_Id { get; set; }

        public int Points { get; set; }

        public Int64 Rank { get; set; }

        public Int64 Timestamp { get; set; }
    }
}
