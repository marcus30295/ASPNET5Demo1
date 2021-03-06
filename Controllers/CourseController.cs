﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET5Demo1.Models;
using Microsoft.AspNetCore.Authorization;
using Omu.ValueInjecter;
using ASPNET5Demo1.Helper;

namespace ASPNET5Demo1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]//內建ModelBinding
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        private readonly JwtHelper _jwtHelper;
        public CourseController(ContosoUniversityContext context, JwtHelper jwtHelper)
        {
            db = context;
            _jwtHelper = jwtHelper;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await db.Courses.ToListAsync();
        }

        //[HttpGet("empty")]
        //public IActionResult Empty()
        //{
        //    return new JsonResult("true");
        //}

        [HttpGet("empty")]
        [Produces("application/json")]
        public IActionResult Empty()
        {
            throw new Exception();
            //return new JsonResult("true");
        }

        [HttpGet("credits/{credit}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByCredit(int credit)
        {
            return await db.Courses.Where(p => p.Credits == credit).ToListAsync();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            return await db.Courses.FindAsync(id);
        }

        // PUT: api/Course/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public  async Task<IActionResult>  PutCourse(int id, CourseUpdateModel course)
        {
            var c = db.Courses.Find(id);
            //valueInjecter
            c.InjectFrom(course);
            await db.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Course
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            db.Courses.Add(course);
            await db.SaveChangesAsync();

            return Created("/api/Course/" + course.CourseId, course);
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public  async Task<IActionResult> DeleteCourse(int id)
        {
          

             var c = db.Courses.Find(id);
            db.Courses.Remove(c);

           await db.SaveChangesAsync();

            return Ok(c);
        }

           // DELETE: api/Course/5
        [HttpDelete("all")]
        public  async Task<IActionResult> DeleteCourseAll()
        {
          

             
            await db.Database.ExecuteSqlRawAsync($"Delete from db.Course");

           //await db.SaveChangesAsync();

            return null;
        }

      
    }
}
