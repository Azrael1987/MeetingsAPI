using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  System.ComponentModel.DataAnnotations;

namespace MeetingsAPI.Models
{
    public class MeetupDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        public string Organizer { get; set; }
        [Required]
        [DataType("YYYY-MM-DD hh:mm:ss")]
        public DateTime Date { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
    }
}
