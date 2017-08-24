using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Models
{
    public class QuizGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ApplicationUser Owner { get; set; }

        public ICollection<Quiz> Quizes { get; set; }
        
        public ICollection<ApplicationUser> Members { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public QuizGroup()
        {
            Quizes = new HashSet<Quiz>();
            Members = new HashSet<ApplicationUser>();
            Tags = new HashSet<Tag>();
        }
    }
}