using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Rating
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Range(1.0, 5.0)]
        public double Value { get; set; }

        public ApplicationUser User { get; set; }

        public Quiz Quiz { get; set; }
    }
}