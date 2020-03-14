using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingsAPI.Enitities;
using MeetingsAPI.Models;

namespace MeetingsAPI.Services
{
    public interface ILectureService
    {
        string CreateLecture(string meetupName, LectureDto model);
        List<LectureDto> GetListOfLecturesPerMeetup(string meetupName);
    }
}
