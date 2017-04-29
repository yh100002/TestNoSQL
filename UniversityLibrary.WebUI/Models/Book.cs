using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoRepository;

namespace UniversityLibrary.WebUI.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        public List<string> Authors { get; set; }
    }
}