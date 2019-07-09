using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Work_with_Entity.Models;

using System.Data.Entity;

namespace Work_with_Entity.InitilizerData
{
    public class SchoolInitializer:DropCreateDatabaseIfModelChanges<ShoolContext>
    {
        /*Entity Framework может автоматически создать базу данных при запуске 
         * приложения. Вы можете указать, что это должно выплоняться при каждом запуске
         * приложения или только тогда, когда модель рассинхронизирована с существующей
         * базой. Вы можете также написать класс с методом, который Entity Framework
         * будет автоматически вызывать перед созданием базы для использования её с
         * тестовыми данными. Мы укажем, что база должна удаляться и пересоздаваться
         * при изменении модели.
         * 
         * 
         * 
         * */


        protected override void Seed(ShoolContext context)
        {
            /*
             * Метод Seed принимает объект контекста базы как входящий параметр 
             * и использует его для добавления новых сущностей в базу. Для каждого типа
             * сущности код создает коллекцию новых сущностей, добавляя их в 
             * соответствующее свойство DbSet, и потом сохраняет изменения в базу.
             * Нет необходимости в вызове SaveChanges после каждой группы сущностей,
             * как сделано у нас, но это помогает определить проблему в случае
             * возникновения исключений.
             * 
             * 
             * */


            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso",    EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstName = "Arturo",   LastName = "Anand",     EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { FirstName = "Gytis",    LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstName = "Yan",      LastName = "Li",        EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstName = "Peggy",    LastName = "Justice",   EnrollmentDate = DateTime.Parse("2001-09-01") },
                new Student { FirstName = "Laura",    LastName = "Norman",    EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { FirstName = "Nino",     LastName = "Olivetto",  EnrollmentDate = DateTime.Parse("2005-09-01") }
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course { Title = "Chemistry",      Credits = 3, },
                new Course { Title = "Microeconomics", Credits = 3, },
                new Course { Title = "Macroeconomics", Credits = 3, },
                new Course { Title = "Calculus",       Credits = 4, },
                new Course { Title = "Trigonometry",   Credits = 4, },
                new Course { Title = "Composition",    Credits = 3, },
                new Course { Title = "Literature",     Credits = 4, }
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentID = 1, CourseID = 1, Grade = 1 },
                new Enrollment { StudentID = 1, CourseID = 2, Grade = 3 },
                new Enrollment { StudentID = 1, CourseID = 3, Grade = 1 },
                new Enrollment { StudentID = 2, CourseID = 4, Grade = 2 },
                new Enrollment { StudentID = 2, CourseID = 5, Grade = 4 },
                new Enrollment { StudentID = 2, CourseID = 6, Grade = 4 },
                new Enrollment { StudentID = 3, CourseID = 1            },
                new Enrollment { StudentID = 4, CourseID = 1,           },
                new Enrollment { StudentID = 4, CourseID = 2, Grade = 4 },
                new Enrollment { StudentID = 5, CourseID = 3, Grade = 3 },
                new Enrollment { StudentID = 6, CourseID = 4            },
                new Enrollment { StudentID = 7, CourseID = 5, Grade = 2 },
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }


    }
}