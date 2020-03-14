using System.ComponentModel.DataAnnotations;

namespace MeetingsAPI.Models
{
    public class LectureDto
    {
        [Required]
        [MinLength(5)]
        public string Author { get; set; }
        [Required]
        [MinLength(5)]
        public string Topic { get; set; }
        public string Description { get; set; }
        [Required]
        [MinLength(2)]
        public string Language { get; set; }
    }
}
