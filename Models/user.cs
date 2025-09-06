using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_projects.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
<<<<<<< HEAD
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
=======

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

>>>>>>> 767c2f9656b75a5d60357e27db48943363454024
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
<<<<<<< HEAD
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
=======

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
>>>>>>> 767c2f9656b75a5d60357e27db48943363454024
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}