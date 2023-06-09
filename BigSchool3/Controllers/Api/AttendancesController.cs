﻿using BigSchool3.DTOs;
using BigSchool3.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace BigSchool3.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
       
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
            {
                return BadRequest("The Attendace already exists !");
            }
           
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId,

            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
        // DELETE: api/Attendances/5
        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _dbContext.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.CourseId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            _dbContext.Attendances.Remove(attendance);
            _dbContext.SaveChanges();

            return Ok(attendance);
        }
    }
}
