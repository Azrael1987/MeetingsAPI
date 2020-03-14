using AutoMapper;
using MeetingsAPI.Enitities;
using MeetingsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingsAPI
{
    public class MeetupProfile : Profile
    {
        public MeetupProfile()
        {
            // z tymi samymi nazwami mappuje automatycznie
            //CreateMap<Meetup, MeetupDetailsDto>().ForMember(m => m.Date, map => map.MapFrom(meetup => meetup.Date));

            CreateMap<Meetup, MeetupDetailsDto>()
                .ForMember(m => m.City, map => map.MapFrom(meetup => meetup.Location.City))
                .ForMember(m => m.PostCode, map => map.MapFrom(meetup => meetup.Location.PostCode))
                .ForMember(m => m.Street, map => map.MapFrom(meetup => meetup.Location.Street))
                .ReverseMap();
          //      .ForMember(m => m.Lectures, map => map.MapFrom(meetup => meetup.Lectures));

            CreateMap<MeetupDto, Meetup>();
            CreateMap<LectureDto, Lecture>().ReverseMap(); // mapowanie dwukierunkowe
        }
    }
}
