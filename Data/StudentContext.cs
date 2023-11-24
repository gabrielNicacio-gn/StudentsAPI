using Microsoft.EntityFrameworkCore;
using StudentsApi.Entitys;

namespace StudentsApi.Data
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlite("Data Source=db_Students");
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
