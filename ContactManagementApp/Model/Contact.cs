using System.ComponentModel.DataAnnotations;

namespace ContactManagementApp.Model
{
    public class Contact
    {
        [Key]

        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
