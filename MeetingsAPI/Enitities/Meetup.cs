using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI.Enitities
{
    public class Meetup
    { 
        public int Id { get; set;}
        public string Name { get; set;}
        public string Organizer { get; set;}
        public DateTime Date { get; set;}
        public bool IsPrivate { get; set;}


        //1 -1
        public virtual Location Location { get; set;}
        //1 - wiele
        public virtual List<Lecture> Lectures { get; set;}
        //wiele - 1
        public virtual SpecialGuestJoint SpecialGuestJoint { get; set; }
        public int SpecialGuestJointId { get; set; }
        // public virtual List<SpecialGuest> SpecialGuestsJoint { get; set;}
    }
}
