using BigSchool3.Models;
using BigSchool3.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool3.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
       public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var vieModel = new CourseViewModel
            {
                Categoies = _dbContext.Categories.ToList()
            };
            return View(vieModel);
            
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categoies = _dbContext.Categories.ToList();
                return View("Create",viewModel);
            } 
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime=viewModel.GetDateTime(),
                CategoryId=viewModel.Category,
                Place=viewModel.Place

            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Home");

        }
    }
}