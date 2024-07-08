using Microsoft.EntityFrameworkCore;
using StoredProcedure_CRUD.Models;
using System.Collections.Generic;

namespace StoredProcedure_CRUD 
{
   public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Student> SpTable{ get; set; }
        }
}
