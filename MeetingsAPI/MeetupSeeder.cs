using MeetingsAPI.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingsAPI
{
    public class MeetupSeeder
    {
        private readonly MeetupContext _meetupContext;

        public MeetupSeeder(MeetupContext meetupContext)
        {
            _meetupContext = meetupContext;
        }

        public void Seed()
        {
            if (_meetupContext.Database.CanConnect())
            {
                if (!_meetupContext.Meetups.Any())
                {
                    InsertSampleDate();
                }
            }
        }

        private void InsertSampleDate()
        {
            var meetups = new List<Meetup>
            {
                new Meetup
                {
                    Name = "FE's world",
                    Date = DateTime.Now.AddDays(24),
                    Organizer = "Angular company",
                    IsPrivate = false,
                    Location = new Location
                    {
                        City = "Katowice",
                        Street = "Dluga 12",
                        PostCode = "25-900"
                    },
                    Lectures = new List<Lecture>
                    {
                        new Lecture
                        {
                            Author = "Jim Brown",
                            Description = "Easy world with Angular",
                            Topic = "How use Angular 8.0",
                            Language = "English"
                        },
                        new Lecture
                        {
                            Author = "Tim Owen",
                            Description = "Adventures with Angular",
                            Topic = "Secrets of Angular 8.0",
                            Language = "English"
                        }

                    }
                },
                {
                    new Meetup
                    {
                        Name ="UnitTest's world",
                        IsPrivate = true,
                        Date = DateTime.Now.AddDays(10),
                        Organizer = "TDD company",
                        Location = new Location
                        {
                            City = "Lublin",
                            PostCode = "12-230",
                            Street = "Leśna 34"
                        },
                        Lectures = new List<Lecture>
                        {
                            new Lecture
                            {
                                Author = "Jan Nowak",
                                Topic = "How create good unitTests",
                                Description = "Everything about good UnitTest",
                                Language = "Polish",                                
                            },
                            new Lecture
                            {
                                Author = "Iwan Stopkov",
                                Topic = "How create good Mocks",
                                Language = "Russian",
                                Description = "5 golden rules about create Mock",
                            }
                        }

                    }
                }
            };

            _meetupContext.AddRange(meetups);
            _meetupContext.SaveChanges();
        }
    }
}
