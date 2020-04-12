using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingsAPI.Enitities
{
    public class SpecialGuest
    {
        [Key, Required]
        public int SpecialGuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Citzenship { get; set; }

        public virtual List<SpecialGuestJoint> FirstSpecialGuestJoints { get; set; } = new List<SpecialGuestJoint>();
        public virtual List<SpecialGuestJoint> SecondSpecialGuestJoints { get; set; } = new List<SpecialGuestJoint>();
    }
}
