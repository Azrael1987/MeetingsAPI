using AutoMapper;
using MeetingsAPI.Enitities;
using MeetingsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MeetingsAPI.Services
{
    public class LectureService : ILectureService
    {
        private MeetupContext _meetupContext;
        private IMapper _mapper;
        private ILogger<LectureService> _logger;

        public LectureService(MeetupContext meetupContext, IMapper mapper, ILogger<LectureService> logger)
        {
            _mapper = mapper;
            _meetupContext = meetupContext;
            _logger = logger;
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

        public void DeleteAllLectures(string meetupName)
        {
            var meetup = _meetupContext.Meetups.
                Include(m => m.Lectures)
                .FirstOrDefault(m => m.Name.ToLower().Replace(" ", "-") == meetupName.ToLower());

            if (meetup == null)
            {
                throw new NullReferenceException();
            }

            _logger.LogWarning($"All lectures for meetup {meetup.Name} deleted");
            _meetupContext.Lectures.RemoveRange(meetup.Lectures);
            _meetupContext.SaveChanges(); 
        }

        public void DeleteLecture(string meetupName, int lectureId)
        {
            var meetup = _meetupContext.Meetups
                .Include(m => m.Lectures)
                .FirstOrDefault(m => m.Name.ToLower().Replace(" ", "-") == meetupName.ToLower());

            if(meetup == null || !meetup.Lectures.Any())
            {
                _logger.LogWarning($"Not found lecture {lectureId} for meetup {meetup.Name}");
                throw new NullReferenceException();
            }

            var lecture = meetup.Lectures.FirstOrDefault(l => l.Id == lectureId);
            if(lecture == null)
            {
                throw new NullReferenceException();
            }

            _meetupContext.Lectures.Remove(lecture);
            _meetupContext.SaveChanges();
        }

    }
}
