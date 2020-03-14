using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI.Enitities
{
    public class SpecialGuest
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string Company { get; set;}
        public string Citzenship { get; set;}

        //public virtual Meetup Meetup { get; set;}
        //public int MeetupId { get;set;}
        public virtual List<SpecialGuestJoint> FirstSpecialGuestJoints { get; set;}
        public virtual List<SpecialGuestJoint> SecondSpecialGuestJoints { get; set; }


    }
}
