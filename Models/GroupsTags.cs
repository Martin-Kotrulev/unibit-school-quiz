namespace App.Models
{
    public class GroupsTags
    {
        public int GroupId { get; set; }

        public QuizGroup Group { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}