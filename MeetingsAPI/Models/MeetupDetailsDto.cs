using System;
using System.Collections.Generic;

namespace MeetingsAPI.Models
{
    public class MeetupDetailsDto: MeetupDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }

        public List<LectureDto> Lectures { get; set; }
    }
}
