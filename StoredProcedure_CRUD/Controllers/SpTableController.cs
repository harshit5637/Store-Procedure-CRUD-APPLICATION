using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using StoredProcedure_CRUD;
using StoredProcedure_CRUD.Models;
using System.Data;

namespace StoreProcedureWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllCharacter()
        {
            var result = await _context.SpTable.ToListAsync();
            return Ok(result);
        }

        //Purpose: This action method fetches a list of students from the database filtered by name using a stored procedure.
        // Get Student By Name 
        [HttpGet("getstudentbyname")]
        public async Task<ActionResult<List<Student>>> GetStudentByName(string name)
        {
            //SqlParameter Initialization:
            var parameters = new[]
            {
        new SqlParameter("@Name", name)
    };
            //Executing Stored Procedure:
            //_context refers to the DbContext instance that provides access to the database. 
            //FromSqlRaw is used to execute raw SQL queries, and in this case,
            //it executes the stored procedure GetStudentByName with the parameter @Name.
            var result = await _context.SpTable.FromSqlRaw("EXEC GetStudentByName @Name", parameters).ToListAsync();

            return Ok(result);
        }


        // Update student id and name 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, String  name)
        {
            var parameters = new[]
            {
        new SqlParameter("@Id", id),
        new SqlParameter("@Name", name)
    };

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateStudentName @Id, @Name", parameters);

            return NoContent();
        }


        //Delete any Id with the help of their id 


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var parameters = new[]
           {
                new SqlParameter("@Id", id)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteStudent @Id", parameters);
             
            return NoContent();
        }

        // Add Student in the table

        [HttpPost("Add Student")]
       public async Task<IActionResult> AddStudent(Student student)
       {
           var parameters = new[]
           {
               new SqlParameter("@Id", student.Id),
               new SqlParameter("@Age", student.Age),
               new SqlParameter("@Name",student.Name)
           };

           await _context.Database.ExecuteSqlRawAsync("EXEC CreateStudent @Id, @Age,@Name", parameters);

        return CreatedAtAction("AddStudent", new { id = student.Id }, student);
       }
    }
}




//[HttpGet("SP")]
//public async Task<ActionResult<List<Student>>> GetAllStudent()
//{
//    var result = await _context.SpTable.FromSqlRaw("GetStudentByName").ToListAsync();
//    return Ok(result);
//}
