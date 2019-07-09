using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Work_with_Entity.Models
{
    public class ShoolContext:DbContext
    {

        /*Главный класс, координирующий функциональность Entity Framework для
         *  текущей модели данных называется database context. 
         *  Данный класс наследуется от System.Data.Entity.DbContext. 
         *  В коде вы определяете, какие сущности включить в модель данных, 
         *  и также можете определять поведение самого Entity Framework. 
         *  В нашем коде этот класс имеет название SchoolContext.
         * 
         * */

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }


        /*
         * Код создаёт свойство DbSet для каждого множества сущностей. 
         * В терминологии Entity Framework множество сущностей (entity set) относится 
         * к таблице базы данных, и сущность относится к записи в таблице.

            Содержимое метода OnModelCreating защищает имена таблиц от плюрализации, 
            и, если вы этого не делаете, то получаете такие имена таблиц, как Students,
            Courses, Enrollments. В ином случае имена таблиц будут Student, Course, Enrollment.
            Разработчики спорят на тему того, нужно ли плюрализовывать имена таблиц или нет. 
            Мы используем одиночную форму, но важен тот момент, что вы можете выбрать, 
            включать эту строчку в код или нет.

            (Этот класс находится в namespace Models потому, что в некоторых ситуациях подход 
            Code First подразумевает нахождение классов сущностей и контекста в одном и том же
            namespace.)
         * 
         * */



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}