using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Services;
using Models.Models;
using WebApi.Dto;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: Course/Courses
        [HttpGet]
        public ViewResult Courses()
        {
            IEnumerable<CourseDto> model = _courseService.GetAllCourses()
                .Select(course => CourseDto.FromModel(course));
            return View(model);
        }
            
        // GET Course/Edit/22
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var course = _courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound(); 
            }

            var model =  CourseDto.FromModel(course);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CourseDto courseDto)
        {
            if(courseDto == null)
            {
                return BadRequest();
            }
            _courseService.UpdateCourse(courseDto.ToModel());
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _courseService.DeleteCourse(id);
            return RedirectToAction("Courses");
        }

        // GET Course/Create/22
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            var model = new CourseDto() { StartDate = System.DateTime.Now, EndDate = System.DateTime.Now };
            return View("Edit",model);
        }

        [HttpPost]
        public IActionResult Create(CourseDto courseDto)
        {

            if (courseDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", courseDto);
            }

            if (courseDto.StartDate > courseDto.EndDate)
            {
                ModelState.AddModelError("EndDate", "Start date cannot be after end date");
            }

            _courseService.CreateCourse(courseDto.ToModel());
            return RedirectToAction("Courses");
        }
    }
}
