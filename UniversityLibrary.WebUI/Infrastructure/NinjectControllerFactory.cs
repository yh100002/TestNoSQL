using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using UniversityLibrary.WebUI.Models;

namespace UniversityLibrary.WebUI.Infrastructure
{
    //D.I Factory class
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,Type controllerType)
        {
            //called by
            return controllerType == null? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {           
            //Registering Data Repository with interface and concrete class and controller
            //It will be triggered and init automatically
            ninjectKernel.Bind<ILibraryRepository>().To<LibraryRepository>();            
        }
    }
}