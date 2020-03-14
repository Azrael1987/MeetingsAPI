using MeetingsAPI.Enitities;
using MeetingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI.Services
{
    public interface IMeetupService
    {
        List<MeetupDetailsDto> GetMeetupsList();
        List<MeetupDetailsDto> GetMeetupsWithDetailsList();
        MeetupDetailsDto GetMeetupWithDetailsList(string meetupName);
        MeetupDetailsDto GetMeetup(string name);
        string CreateMeetup(MeetupDto data);
        string Edit(string name, MeetupDto model);
        void DeleteMeetup(string name);
    }
}
