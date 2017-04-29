using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoRepository;

namespace UniversityLibrary.WebUI.Models
{
    public class User : Entity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
               
        public string Password { get; set; }
        
        public List<Book> Books { get; set; }

        public User()
        {
            Books = new List<Book>();
        }
    }
}