using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Work_with_Entity.Models;
using Work_with_Entity.InitilizerData;
using System.Data.Entity;

namespace Work_with_Entity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //В методе Application_Start вызовите метод Entity Framework, 
            //который запускает код инициализации базы

            Database.SetInitializer<ShoolContext>(new SchoolInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
