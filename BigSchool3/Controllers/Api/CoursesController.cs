﻿using BigSchool3.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool3.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course=_dbContext.Courses.Single(c=>c.Id==id && c.LecturerId==userId);
            if (course.IsCanceled)
            {
                return NotFound();
            }
            course.IsCanceled = true;

            //var notification=new Notification()
            //{
            //    DateTime = DateTime.Now,
            //    Course=course,
            //    Type=NotificationType.CourseCanceled
            //};
            //var attendees = _dbContext.Attendances.Where(a => a.CourseId == course.Id)
            //    .Select(a => a.Attendee).ToList();
            //foreach(var attendance in attendees)
            //{
            //    var userNotification = new UserNotification()
            //    {
            //        User = attendance,
            //        Notification = notification
            //    };
            //    _dbContext.userNotifications.Add(userNotification);
            //}
            _dbContext.SaveChanges();
            return Ok();
        }

       
    }
}
