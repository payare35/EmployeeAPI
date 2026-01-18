using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id {get; set;}

        [Required]
        [Column("email")]
        public required string Email {get; set;}

        [Required]
        [Column("password_hash")]
        public required string PasswordHash {get; set;}
    }
}