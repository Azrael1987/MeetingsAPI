using AutoMapper;
using MeetingsAPI.Enitities;
using MeetingsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MeetingsAPI.Services
{
    public class LectureService : ILectureService
    {
        private MeetupContext _meetupContext;
        private IMapper _mapper;

        public LectureService(MeetupContext meetupContext, IMapper mapper)
        {
            _mapper = mapper;
            _meetupContext = meetupContext;
        }

        public string CreateLecture(string meetupName, LectureDto model)
        {
            var meetups = _meetupContext.Meetups
                .Include(m => m.Lectures)
                .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == meetupName.ToLower());
            if (meetups == null)
            {
                return string.Empty;
                // NotFound()
            }

            var lecture = _mapper.Map<Lecture>(model);
            meetups.Lectures.Add(lecture);
            _meetupContext.SaveChanges();

            return $"api/meetup/" + meetupName;
        }

        public List<LectureDto> GetListOfLecturesPerMeetup(string meetupName)
        {
            var meetup = _meetupContext.Meetups
                 .Include(m => m.Lectures)
                 .FirstOrDefault(m => m.Name.ToLower().Replace(" ", "-") == meetupName.ToLower());

            if (meetup == null)
            {
                throw new NullReferenceException();
            }

            return _mapper.Map<List<LectureDto>>(meetup.Lectures);
        }
    }
}
