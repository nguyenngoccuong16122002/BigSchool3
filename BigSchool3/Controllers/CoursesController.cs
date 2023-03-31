using BigSchool3.Models;
using BigSchool3.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                Categoies = _dbContext.Categories.ToList(),
                Heading="Add Course"
            };
            return View("CoureseForm", vieModel);
            
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categoies = _dbContext.Categories.ToList();
                return View("CoureseForm", viewModel);
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
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId=User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId==userId);
            var viewModel = new CourseViewModel {
                Categoies=_dbContext.Categories.ToList(),
                Date=course.DateTime.ToString("dd/MM/yyyy"),
                Time=course.DateTime.ToString("HH:mm"),
                Category=course.CategoryId,
                Place=course.Place,
                Heading="Edit Course",
                Id=course.Id,
            };
            return View("CoureseForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categoies = _dbContext.Categories.ToList();
                return View("CoureseForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == viewModel.Id && c.LecturerId == userId);
            course.Place=viewModel.Place;
            course.DateTime=viewModel.GetDateTime();
            course.CategoryId = viewModel.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return View();
            }
            var course = _dbContext.Courses.Single(c => c.Id==id);
            _dbContext.Courses.Remove(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId=User.Identity.GetUserId();
            var courese=_dbContext.Attendances
                .Where(a=>a.AttendeeId==userId)
                .Select(a=>a.Course)
                .Include(l=>l.Lecturer)
                .Include(l=>l.Category)
                .ToList();
            var viewModel= new CoursesViewModel{
                UpcommingCourses=courese,
                ShowAction=User.Identity.IsAuthenticated,   
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var courese = _dbContext.Courses
                .Where(a => a.LecturerId == userId && a.DateTime > DateTime.Now)

                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            return View(courese);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courese = _dbContext.Courses
                .Where(a => a.LecturerId == userId && a.DateTime > DateTime.Now)

                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            return View(courese);
        }
    }
}