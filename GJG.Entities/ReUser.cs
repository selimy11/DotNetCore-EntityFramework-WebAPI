using System;
using System.Collections.Generic;
using System.Text;

namespace GJG.Entities
{
    public class ReUser
    {
        public Guid User_Id { get; set; }
        public string Display_Name { get; set; }
        public int Points { get; set; }
        public Int64 Rank { get; set; }
    }
}
