using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UniversityLibrary.WebUI.Infrastructure;

namespace UniversityLibrary.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);                      
            //Ninject as Dipendency Injecter : D.I as Ioc
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();
            //Response.Redirect("/Error/" + exception.Message);
            WritingEventLog(exception.Message); //Logging into windows event log system for convinience
        }

        public static void WritingEventLog(string msg)
        {
            if (!System.Diagnostics.EventLog.SourceExists("University Library Service"))
            {
                System.Diagnostics.EventLog.CreateEventSource("University Library Service", "Application");
            }
            System.Diagnostics.EventLog.WriteEntry("University Library Service", msg);      
        }
    }
}
