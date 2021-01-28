using System;
using System.ComponentModel.DataAnnotations;

namespace GJG.Entities
{
    public class User
    {
        [Key]
        public Guid User_Id { get; set; }

        [StringLength(100)]
        public string Display_Name { get; set; }

        [StringLength(5)]
        public string Country { get; set; }

    }
}
