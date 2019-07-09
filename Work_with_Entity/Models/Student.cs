using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Work_with_Entity.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Display(Name ="Фамилия")]
        public string LastName { get; set; }

        [Display(Name ="Имя")]
        public string FirstName { get; set; }

        [Display(Name ="Регистрация")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public DateTime EnrollmentDate { get; set; }



    }
}