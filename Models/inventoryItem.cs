using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_projects.Models
{
    public class InventoryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; } = 0.00m;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; } = null!;
    }
}
