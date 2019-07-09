using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_with_Entity.Models;
using PagedList;

namespace Work_with_Entity.Controllers
{
    public class StudentsController : Controller
    {

        ShoolContext db;

        public StudentsController()
        {
            db = new ShoolContext();
        }

        //обычный вывод данных на страницу
        //public ActionResult Index()
        //{
        //    return View(db.Students.ToList());

        //}


        //добавление сортировки
        /*
         * Метод принимает sortOrder как параметр из строки запроса в URL,
         *  который предоставляется ASP.NET как параметр для метода.
         *   Параметр является строкой «Name» или «Date» с (опционально)
         *    последующим пробелом и строкой “desc” для указания того,
         *     что необходимо сортировать по убыванию.

            При первом вызове страницы Index строки запроса нет,
            и студенты отображаются в порядке по возрастанию LastName,
            что указано как вариант по умолчанию в switch.
            После того, как пользователь щелкает на заголовке столбца,
            соответствующее значение sortOrder добавляется в строку запроса.
         * 
         * 
         * */
        //------------------------------------------ 
        //public ViewResult Index(string SortOrder)
        //{
        //    /*
        //     * Это тернарное утверждения. Первое утверждает, что, если sortOrder
        //     *  равен null или пустой, то значение ViewBag.NameSortParam
        //     *   устанавливается в “Name desc”,
        //     *    иначе устанавливается в пустую строку.
        //     * */
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "Name dec" : "";
        //    ViewBag.DateSortParm = SortOrder == "Date" ? "Date desc" : "Date";
        //    var students = from s in db.Students
        //                   select s;
        //    switch(SortOrder)
        //    {
        //        case "Name dec":
        //            students = students.OrderByDescending(s => s.LastName);
        //            break;
        //        case "Date":
        //            students = students.OrderBy(s => s.EnrollmentDate);
        //            break;
        //        case "Date desc":
        //            students = students.OrderByDescending(s => s.EnrollmentDate);
        //            break;
        //        default:
        //            students = students.OrderBy(s => s.LastName);
        //            break;
        //    }

        //    return View(students.ToList());

        //}
        ////-----------------------------------------------

        //----------------------добавление сортировки и строки поиска-----------
        /*
         * Мы добавили в метод Index параметр searchString,
         *  кляузу в LINQ-утверждение, с помощью которого выбираются
         *   только те студенты, имя или фамилия которых содержит строку поиска.
         *    Строка поиска получается из текстового поля, которое вы позже добавите
         *     на представление. Код, который добавляет кляузу where в запрос,
         *      выполняется только в том случае, если задано значение для поиска
         * 
         * */
        //public ViewResult Index(string SortOrder,string searchString)
        //{
        //    /*
        //     * Это тернарное утверждения. Первое утверждает, что, если sortOrder
        //     *  равен null или пустой, то значение ViewBag.NameSortParam
        //     *   устанавливается в “Name desc”,
        //     *    иначе устанавливается в пустую строку.
        //     * */
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "Name dec" : "";
        //    ViewBag.DateSortParm = SortOrder == "Date" ? "Date desc" : "Date";
        //    var students = from s in db.Students
        //                   select s;
        //    /*
        //     * Реализация Contains .NET Framework возвращает все записи в том случае,
        //     *  когда вы передаете в него пустую строку, но провайдер Entity Framework
        //     *   для SQL Server Compact 4.0 для пустой строки возвращает пустое
        //     *    множество. Кроме этого, реализация в .NET Framework проводит регистрозависимое 
        //     *    сравнение, в отличие от провайдеров Entity Framework SQL Server,
        //     *     которые по умолчанию проводят регистронезависимое сравнение.
        //     * 
        //     * 
        //     * */

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
        //                                      || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
        //    }


        //    switch (SortOrder)
        //    {
        //        case "Name dec":
        //            students = students.OrderByDescending(s => s.LastName);
        //            break;
        //        case "Date":
        //            students = students.OrderBy(s => s.EnrollmentDate);
        //            break;
        //        case "Date desc":
        //            students = students.OrderByDescending(s => s.EnrollmentDate);
        //            break;
        //        default:
        //            students = students.OrderBy(s => s.LastName);
        //            break;
        //    }

        //    return View(students.ToList());

        //}
        //--------------------------------------------------------------------------


        //Добавление функциональности разбиения по страницам в метод Index
        //-----------------------------------------------------------------------
        public ViewResult Index(string sortOrder, string currentFilter,string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "Date desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            /*
             * В конце метода запрос конвертируется вместо List в PagedList,
             *  после чего его можно передавать в представление,
             *   поддерживающее разбиение результатов по страницам.
             * 
             * 
             * В метод ToPagedList передаётся значение индекса страницы,
             *  которое равно 0, в отличие от номера страницы, который равен 1.
             * Поэтому код извлекает 1 из номера страницы, чтобы получить значения
             * индекса страницы (два знака вопроса обозначают оператор,
             * определяющий значение по умолчанию для типа nullable,
             * таким образом, выражение (page ?? 1) возвращает значение page
             * в том случае, если оно имеет значение, или 1, если page равен
             * null. Другими словами, установите pageIndex в page - 1 если
             * page не равен null, или установите его в 1-1 если он равен null)
             * */
            int pageSize = 3;
           
            if (page <= 0)
                page = 1;
            int pageIndex = (page ?? 1) - 1;
            return View(students.ToPagedList(pageIndex+1, pageSize));

        }
       //-------------------------------------------------------------------




        public ActionResult Details(int? id)

        {

            if(id==null)
            {
                return HttpNotFound();
            }
            
            Student student = db.Students.Find(id);

            return View(student);

        }


        /*
         * Таким образом мы добавляем сущность Student , созданную ASP.NET MVC
         *  Model Binder в соответствующее множество сущностей и затем сохраняем
         *   изменения в базу. (Model binder – функциональность ASP.NET MVC,
         *    облегчающая работа с данными, пришедшими из формы. Model binder
         *     конвертирует данные из формы в соответствующие типы данных .NET
         *      Framework и передает в нужный метод как параметры. 
         *      В данном случае, model binder инстанциирует сущность Student
         *       используя значения свойства из коллекции Form.)
         * 
         * */

        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }

            catch(DataException)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(student);
           
        }


        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if(Id==null)
            {
                return HttpNotFound();
            }

            Student student = db.Students.Find(Id);
            return View(student);

        }

        /*
         * Код похож на то, что было в методе HttpPost Create,
         * однако вместо добавления сущности в множество этот код устанавливает
         * свойство сущности, определяющее, было ли оно изменено.
         * При вызове SaveChanges свойство Modified указывает Entity Framework
         * на необходимость создания SQL запроса для обновления записи в базе.
         * Все столбцы записи будут обновлены, включая те, которые пользователь
         * не трогал. Вопросы параллелизма игнорируются
         * */

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        /*Как и в случае с операциями обновления и создания, операция удаления также
         *  нуждается в двух методах. Метод, вызываемый в ответ на GET-запрос,
         *  показывает пользователю представлению, позволяющее подтвердить или
         *  отменить удаление. Если пользователь подтверждает удаление, 
         *  создаётся POST-запрос и вызывается метод HttpPost Delete.

            Вам необходимо добавить блок исключений try-catch в код метода HttpPost
            Delete для обработки ошибок, которые могут возникнуть при обновлении базы
            данных. Если возникает ошибка, метод HttpPost Delete вызывает метод
            HttpGet Delete, передавая ему параметр, сигнализирующий об ошибке.
            Метод HttpGet Delete снова генерирует страницу подтверждения удаления
            и текстом ошибки.
         * Этот код принимает опциональный параметр булевого типа,
         *  сигнализирующий о возникновении ошибки. Этот параметр равен null (false)
         *   после вызова HttpGet Delete и true при вызове HttpPost Delete.
         * 
         * */

        [HttpGet]
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Что то пошло не так обратитесь к админу!!!";
            }
            return View(db.Students.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)

        {

            if (disposing)

                db.Dispose();

            base.Dispose(disposing);

        }



    }
}
 