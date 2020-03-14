using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI.Enitities
{
    public class SpecialGuestJoint
    {
        [Key, Required]
        public int Id { get; set;} 

        public virtual SpecialGuest FirstSpecialGuest { get; set; }
        public int FirstSpecialGuestId { get; set; }
        public virtual SpecialGuest SecondSpecialGuest { get; set; }
        public int SecondSpecialGuestId { get; set; }
        public virtual List<Meetup> Meetups { get; set;}
    }
}
