using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoRepository;

namespace UniversityLibrary.WebUI.Models
{
    public class LibraryRepository : ILibraryRepository
    {
        static MongoRepository<Book> bookRepo = new MongoRepository<Book>();
        static MongoRepository<User> userRepo = new MongoRepository<User>();     

        IEnumerable<Book> ILibraryRepository.Books
        {
            get
            {
                return bookRepo.AsEnumerable<Book>();
            }
        }

        IEnumerable<User> ILibraryRepository.Users
        {
            get
            {
                return userRepo.AsEnumerable<User>();
            }
        }

        void ILibraryRepository.UpdateUser(User user)
        {
            var u = userRepo.Update(user);   
        }
    }
}