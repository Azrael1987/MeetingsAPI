using MeetingsAPI.Enitities;
using MeetingsAPI.Models;
using MeetingsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MeetingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private readonly IMeetupService _meetupService;

        public MeetupController(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        /// <summary>Pobiera listę spotkań wraz z lokalizacjami </summary>
        /// GET api/meetup
        /// <returns>
        /// [
        ///     {
        ///      "name": "FE's world",
        ///      "organizer": "Angular company",
        ///      "date": "2020-03-23T01:24:45.6257064",
        ///      "isPrivate": false,
        ///      "city": "Katowice 2",
        ///      "street": "Dluga 12",
        ///      "postCode": "25-900"
        ///     },
        ///     {
        ///      "name": "UnitTest's world",
        ///      "organizer": "TDD company",
        ///      "date": "2020-03-09T01:24:45.6280575",
        ///      "isPrivate": true,
        ///      "city": "Lublin",
        ///      "street": "Leśna 34",
        ///      "postCode": "12-230"
        ///     }
        /// ]
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<MeetupDetailsDto>), StatusCodes.Status200OK)]
        public ActionResult<List<MeetupDetailsDto>> Get()
        {
            //return StatusCode(200, _meetupService.GetMeetupsList());
            return Ok(_meetupService.GetMeetupsList());
            // return _meetupService.GetMeetupsList();
        }

        /// <summary>Pobiera listę spotkań wraz z lokalizacjami </summary>
        /// GET api/meetup
        /// <returns>
        ///       [
        ///           {
        ///       "city": "Katowice 2",
        ///               "street": "Dluga 12",
        ///               "postCode": "25-900",
        ///               "lectures": 
        ///               [
        ///           {
        ///               "author": "Tim Owen",
        ///               "topic": "Secrets of Angular 8.0",
        ///               "description": "Adventures with Angular",
        ///               "language": "English"
        ///           },
        ///           {
        ///               "author": "Tomasz Rosin",
        ///               "topic": "Bezpieczeniństwo w sieci",
        ///               "description": "10 zasad bezpieczeństwa sieciowego",
        ///               "language": "polski"
        ///           }
        ///                ],
        ///                         
        ///                "id": 1,
        ///                "name": "FE's world",
        ///                "organizer": "Angular company",
        ///                "date": "2020-03-23T01:24:45.6257064",
        ///                "isPrivate": false
        ///            },
        ///            {
        ///                "city": "Lublin",
        ///                "street": "Leśna 34",
        ///                "postCode": "12-230",
        ///                "lectures": 
        ///                [
        ///                    {
        ///                        "author": "Jan Nowak",
        ///                        "topic": "How create good unitTests",
        ///                        "description": "Everything about good UnitTest",
        ///                        "language": "Polish"
        ///                    },
        ///                    {
        ///                        "author": "Iwan Stopkov",
        ///                        "topic": "How create good Mocks",
        ///                        "description": "5 golden rules about create Mock",
        ///                        "language": "Russian"
        ///                    }
        ///                ],
        ///                "id": 2,
        ///                "name": "UnitTest's world",
        ///                "organizer": "TDD company",
        ///                "date": "2020-03-09T01:24:45.6280575",
        ///                "isPrivate": true
        ///            },
        ///            {
        ///                "city": null,
        ///                "street": null,
        ///                "postCode": null,
        ///                "lectures": [],
        ///                "id": 3,
        ///                "name": "JsDay",
        ///                "organizer": "JS Company",
        ///                "date": "2020-04-15T13:10:00",
        ///                "isPrivate": false
        ///            },
        ///            {
        ///                "city": null,
        ///                "street": null,
        ///                "postCode": null,
        ///                "lectures": [],
        ///                "id": 4,
        ///                "name": "Next Js Day",
        ///                "organizer": "JS Company",
        ///                "date": "2020-04-16T13:10:00",
        ///                "isPrivate": false
        ///            }
        ///            ]
        /// </returns>
        [HttpGet("withdetails")]
        [ProducesResponseType(typeof(List<MeetupDetailsDto>), StatusCodes.Status200OK)]
        public ActionResult<List<MeetupDetailsDto>> GetMeetupsWithDetails()
        {
            return Ok(_meetupService.GetMeetupsWithDetailsList());
        }

        /// <summary>Pobiera listę spotkań wraz z lokalizacjami </summary>
        /// GET api/meetup/UnitTest's-world
        /// <returns>
        ///     {
        ///      "name": "UnitTest's world",
        ///      "organizer": "TDD company",
        ///      "date": "2020-03-09T01:24:45.6280575",
        ///      "isPrivate": true,
        ///      "city": "Lublin",
        ///      "street": "Leśna 34",
        ///      "postCode": "12-230"
        ///     }
        /// </returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(MeetupDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MeetupDetailsDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MeetupDetailsDto), StatusCodes.Status400BadRequest)]
        public ActionResult<MeetupDetailsDto> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var result = _meetupService.GetMeetup(name);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>Tworzy nowy meetup</summary>
        /// POST api/meetup/
        ///     {
        ///      "name": "UnitTest's world",
        ///      "organizer": "TDD company",
        ///      "date": "2020-03-09T01:24:45.6280575",
        ///      "isPrivate": true,
        ///      "city": "Lublin",
        ///      "street": "Leśna 34",
        ///      "postCode": "12-230"
        ///     }
        /// <returns>
        ///     header z URL do stworzonego meetupu
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody]MeetupDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meetupUrl = _meetupService.CreateMeetup(model);
            if (string.IsNullOrEmpty(meetupUrl))
            {
                return BadRequest();
            }
            return Created(meetupUrl, null);
        }
        /// <summary>Aktualizuje meetup</summary>
        /// POST api/meetup/UnitTest's-world
        ///     {
        ///      "name": "UnitTest's world 2",
        ///      "organizer": "TDD company",
        ///      "date": "2020-03-13T01:24:45.6280575",
        ///      "isPrivate": false,
        ///      "city": "Lublin",
        ///      "street": "Leśna 9",
        ///      "postCode": "12-230"
        ///     }
        /// <returns>
        ///     header z URL do edytowanego meetupu
        /// </returns>
        [HttpPut("{name}")]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status404NotFound)]
        public ActionResult Put(string name, [FromBody]MeetupDto model)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(name);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meetupUrl = _meetupService.Edit(name, model);

            if (meetupUrl == null)
            {
                return NotFound(name);
            }
            return NoContent();
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Meetup), StatusCodes.Status404NotFound)]
        public ActionResult Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            try
            {
                _meetupService.DeleteMeetup(name);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
        /// <summary> Pobira konkretny meetup wraz z szczegółami </summary>
        /// GET api/meetup/withdetails/UnitTest's-world
        /// <param name="meetupName"></param>
        /// <returns>
        /// {
        ///     "city": "Lublin",
        ///     "street": "Leśna 34",
        ///     "postCode": "12-230",
        ///     "lectures": [
        ///         {
        ///             "author": "Jan Nowak",
        ///             "topic": "How create good unitTests",
        ///             "description": "Everything about good UnitTest",
        ///             "language": "Polish"
        ///         },
        ///         {
        ///             "author": "Iwan Stopkov",
        ///             "topic": "How create good Mocks",
        ///             "description": "5 golden rules about create Mock",
        ///             "language": "Russian"
        ///         }
        ///     ],
        ///     "id": 2,
        ///     "name": "UnitTest's world",
        ///     "organizer": "TDD company",
        ///     "date": "2020-03-09T01:24:45.6280575",
        ///     "isPrivate": true
        /// }
        /// </returns>
        [HttpGet("withdetails/{meetupName}")]
        public ActionResult<MeetupDetailsDto> GetMeetup(string meetupName)
        {
            if (string.IsNullOrEmpty(meetupName))
            {
                return NotFound();
            }
            MeetupDetailsDto  result;
            try
            {
               result = _meetupService.GetMeetupWithDetailsList(meetupName);
            }
            catch (System.Exception)
            {

                return NotFound();
            }
            return Ok(result);
        }


    }
}