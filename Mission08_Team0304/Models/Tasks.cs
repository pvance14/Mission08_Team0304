using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team0304.Models
{
    public class Tasks
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string? dueDate { get; set; }
        public string Quadrant { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool Completed { get; set; }
    }
}