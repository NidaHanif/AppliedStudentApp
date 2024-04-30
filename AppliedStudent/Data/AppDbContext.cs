using System.Data.Entity;

namespace AppliedStudent.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base()
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
