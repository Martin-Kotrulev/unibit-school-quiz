using System.ComponentModel.DataAnnotations;

namespace App.Core.Models
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}