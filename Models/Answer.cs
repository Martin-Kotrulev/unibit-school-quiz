using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        public bool IsRight { get; set; } = false;

        public Question FromQuestion { get; set; }
    }
}