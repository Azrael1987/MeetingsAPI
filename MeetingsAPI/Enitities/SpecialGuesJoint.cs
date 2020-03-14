﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI.Enitities
{
    public class SpecialGuestJoint
    {
        public virtual List<Meetup> Meetups { get; set;}

        public virtual SpecialGuest FirstSpecialGuest { get; set; }
        public int FirstSpecialGuestId { get; set; }
        public virtual SpecialGuest SecondSpecialGuest { get; set; }
        public int SecondSpecialGuestId { get; set; }
    }
}