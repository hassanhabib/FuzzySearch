using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FuzzyFuzzDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var broker = new StorageBroker();

            IQueryable<Student> students =
                broker.Students.Where(student =>
                    StorageBroker.SoundsLike(student.Name) == StorageBroker.SoundsLike("Ibrahem"));

            Console.Write(students.Count());
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class StorageBroker : DbContext
    {
        public StorageBroker() => this.Database.Migrate();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DemoDB;");

        [DbFunction(Name = "SOUNDEX", IsBuiltIn = true)]
        public static string SoundsLike(string query)
        {
            throw new NotImplementedException();
        }

        public DbSet<Student> Students { get; set; }
    }
}