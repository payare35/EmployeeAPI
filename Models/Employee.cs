using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id {get; set;}

        [Required]
        [Column("name")]
        public required string Name {get; set;}

        [Required]
        [Column("dob")]
        public required DateTime DOB {get; set;}

        [Required]
        [Column("salary")]
        public required decimal Salary {get; set;}

        [Required]
        [Column("contact_no")]
        public required string ContactNo {get; set;}

        
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required DateTime CreatedAt {get; set;}
    }
}