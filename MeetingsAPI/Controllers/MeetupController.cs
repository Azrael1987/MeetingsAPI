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
        public ActionResult<MeetupDetailsDto> Get(string name)
        {
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
            return  NoContent();
        }
    }
}