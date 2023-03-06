using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("User")]
    public class UserDTO
    {
        [Key]
        [Column("Loginname")]
        public string Login { get; set; } = string.Empty;

        [Required]  
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public bool IstAdminBerechtigt { get; set; } = false;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;

        [Required]
        public DateTime TokenErstelltAm { get; set; } = DateTime.Now;
        
        [Required]
        public DateTime TokenLaeuftAbAm { get; set; }
        public bool IstUserAktiv { get; set; } = true;
    }
}
