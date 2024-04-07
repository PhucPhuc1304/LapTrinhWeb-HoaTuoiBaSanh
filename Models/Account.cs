using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CF_HOATUOIBASANH.Models
{
    public class Account
    {
        [Key]     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int AccountID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }

}
