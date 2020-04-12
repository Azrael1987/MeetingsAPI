using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI.Enitities
{
    public class SpecialGuestJoint
    {
        [Key]
        public int Id { get; set;}
        [Required]  // Koncepcja Marka Z. - ale cos tu nie gra
        public int MeetupId { get; set;}
        public virtual Meetup Meetups { get; set;}

        public int FirstSpecialGuestId { get; set; }
        public virtual SpecialGuest FirstSpecialGuests { get; set; }

        public int SecondSpecialGuestId { get; set; }
        public virtual SpecialGuest SecondSpecialGuests { get; set; }
    }
}
