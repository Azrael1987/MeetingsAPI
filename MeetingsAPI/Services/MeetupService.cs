using AutoMapper;
using MeetingsAPI.Enitities;
using MeetingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingsAPI.Services
{
    public class MeetupService : IMeetupService
    {
        MeetupContext _meetupContext;
        IMapper _mapper;

        public MeetupService(MeetupContext meetupContext, IMapper mapper)
        {
            _meetupContext = meetupContext;
            _mapper = mapper;
        }

        public List<MeetupDetailsDto> GetMeetupsList()
        {
            var meetups = _meetupContext.Meetups
                 .Include(m => m.Location)
                 .ToList();
            return _mapper.Map<List<MeetupDetailsDto>>(meetups).ToList();
        }

        public List<MeetupDetailsDto> GetMeetupsWithDetailsList()
        {
            var meetups = _meetupContext.Meetups
                 .Include(m => m.Location)
                 .Include(m => m.Lectures)
                 .ToList();
            return _mapper.Map<List<MeetupDetailsDto>>(meetups).ToList();
        }

        public MeetupDetailsDto GetMeetup(string name)
        {
            Meetup result = _meetupContext.Meetups
                .Include(m => m.Location)
                .Include(m => m.Lectures)
                .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());
            if (result == null)
            {
                return null;
            }

            return _mapper.Map<MeetupDetailsDto>(result);
        }

        public string CreateMeetup(MeetupDto model)
        {
            if (GetMeetup(model.Name) != null)
            {
                return $"api/meetip/" + model.Name.Replace(" ", "-").ToLower();
            }

            Meetup meetup = _mapper.Map<Meetup>(model);
            _meetupContext.Meetups.Add(meetup);
            _meetupContext.SaveChanges();

            var key = meetup.Name.Replace(" ", "-").ToLower();
            return $"api/meetup/" + key;
        }

        public string Edit(string oldName, MeetupDto newModel)
        {
            string actualName = UpdateModel(oldName, newModel);
            return $"api/meetup/" + actualName.Replace(" ", "-").ToLower();
        }

        private string UpdateModel(string oldName, MeetupDto newModel)
        {
            var context = _meetupContext.Meetups.FirstOrDefault(m => m.Name.Replace("-", " ").ToLower() == oldName.ToLower());
            context.Name = newModel.Name;
            context.Organizer = newModel.Organizer;
            context.IsPrivate = newModel.IsPrivate;
            context.Date = newModel.Date;

            _meetupContext.SaveChanges();

            return context.Name;
        }

        public void DeleteMeetup(string name)
        {
           var meetup = _meetupContext.Meetups.FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());

            if(meetup == null)
            {
                 throw new Exception();
            }
            _meetupContext.Remove(meetup);
            _meetupContext.SaveChanges();
        }

        public MeetupDetailsDto GetMeetupWithDetailsList(string meetupName)
        {
           var meetup = _meetupContext.Meetups
                .Include(m => m.Location)
                .Include(m => m.Lectures)
                .FirstOrDefault(m => m.Name.ToLower() == meetupName.ToLower().Replace("-"," "));
            if(meetup == null)
            {
                throw new NullReferenceException();
            }
            return _mapper.Map<MeetupDetailsDto>(meetup);
        }
    }
}
