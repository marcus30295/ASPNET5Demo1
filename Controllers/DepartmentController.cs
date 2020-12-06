using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET5Demo1.Models;
using Omu.ValueInjecter;

namespace ASPNET5Demo1.Controllers
{
      [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController: ControllerBase
    {
         private readonly ContosoUniversityContext db;
          public DepartmentController(ContosoUniversityContext context)
        {
            db = context;
        }

           [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await db.Departments.ToListAsync(); 
        }

        [HttpGet("{id}")]
      public ActionResult<IEnumerable<Course>> GetDepartmentCourses(int id)
        {
            return db.Departments.Include(p => p.Courses)
                .First(p => p.DepartmentId == id).Courses.ToList();

                // return db.Course.Where(x=>x.DepartmentId == id).ToList();
        }
    }
}