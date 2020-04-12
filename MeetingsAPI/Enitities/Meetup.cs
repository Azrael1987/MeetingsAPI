using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingsAPI.Enitities
{
    public class Meetup
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }


        //1 -1
        public virtual Location Location { get; set; }
        //1 - wiele
        public virtual List<Lecture> Lectures { get; set; }

        //wiele - wiele z tabelą łaczącą
        public virtual List<SpecialGuestJoint> SpecialGuestJoints { get; set; } = new List<SpecialGuestJoint>();
    }
}
