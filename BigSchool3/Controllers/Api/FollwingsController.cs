using BigSchool3.DTOs;
using BigSchool3.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BigSchool3.Controllers
{
    [System.Web.Http.Authorize]
    public class FollwingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;   

        public FollwingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult Attend(FollowingDto  followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.followings.Any(a => a.FollowerId == userId && a.FolloweeId == followingDto.FollowingId))
            {
                return BadRequest("The Attendace already exists !");
            }
            var following = new Following
            {
                FollowerId=userId,
                FolloweeId=followingDto.FollowingId,

            };
            _dbContext.followings.Add(following);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}