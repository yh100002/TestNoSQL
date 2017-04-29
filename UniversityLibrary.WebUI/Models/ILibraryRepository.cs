using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoRepository;

namespace UniversityLibrary.WebUI.Models
{
    public interface ILibraryRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Book> Books { get; }

        void UpdateUser(User user);
    }
}
