﻿using MeetingsAPI.Models;
using MeetingsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MeetingsAPI.Controllers
{
    [Route("api/meetup/{meetupName}/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        /// <summary>
        /// <Remark>
        ///  Przykład:
        ///  POST api/meetup/Last-Js-Day/Lecture
        ///  {
        ///  	author: "Jim Bow",
        ///  	topic: "Intro to JS",
        ///  	description: "Intro to JavaScript",
        ///  	language: "PL"
        ///  }
        /// </Remark>
        /// 
        /// </summary>
        /// <param name="meetupName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(string meetupName, [FromBody] LectureDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(_lectureService.CreateLecture(meetupName, model), null);
        }

        [HttpGet]
        public ActionResult<List<LectureDto>> Get(string meetupName)
        {
            if (string.IsNullOrEmpty(meetupName))
            {
                return NotFound();
            }
            List<LectureDto> result;
            try
            {
                result = _lectureService.GetListOfLecturesPerMeetup(meetupName);
            }
            catch (System.Exception)
            {

                return NotFound();
            }
            return result;
        }
    }

    // 6.3 usuwanie wykladów
}