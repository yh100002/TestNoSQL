using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityLibrary.WebUI.Models;

namespace UniversityLibrary.WebUI.Controllers
{    
    public class LibraryController : Controller
    {
        ILibraryRepository repository;

        public int PageSize = 20; //number of books per page
        private static User CurrentUser; //Current user information
        public LibraryController(ILibraryRepository libRepo) // This construtor will be called by Ninjector factory for injection
        {            
            repository = libRepo;
            //int u = Convert.ToInt32(""); // Error Test line
        }

        public ViewResult List(int page = 1)
        {
            //This view model will be passed to the related view
            BooksListViewModel viewmodel = new BooksListViewModel
            {
                Books = repository.Books.OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize),
                //including page information 
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Books.Count()
                }
            };
            return View(viewmodel);
        }

        public ViewResult RenderSearchView()
        {
            //Just display search page
            return View();
        }

        public ViewResult SearchResult(string searchString, int page = 1)
        {
            BooksListViewModel viewmodel = new BooksListViewModel
            {
                //search books based on publisher's name
                Books = repository.Books.Where(s => s.Publisher.Contains(searchString)).OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Books.Count()
                }
            };

            return View(viewmodel);
        }


        public ViewResult Login(string username = "", string password = "")
        {               
            LoginViewModel viewModel = new LoginViewModel
            {
                User = repository.Users.FirstOrDefault(u => u.UserName == username && u.Password == password)
            };

            if (viewModel.User != null) //login success. this is just for login concept.
            {
                //For convience of this simple project, used Session variable for login
                if(Session != null) Session["UserName"] = viewModel.User.UserName;
                CurrentUser = viewModel.User;
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }           

            if (viewModel == null) ModelState.AddModelError("", "Login data is incorrect!");

            return View(viewModel);
        }

        public ActionResult Logout()
        {
            Session["UserName"] = "";  //Just for this simple project          
            return RedirectToAction("List", "Library");
        }

        public ActionResult MyLib()
        {
            //checking for user info again 
            CurrentUser = repository.Users.FirstOrDefault(u => u.UserName == (string)Session["UserName"]);
            BooksListViewModel viewmodel = new BooksListViewModel
            {
                Books = CurrentUser == null ? null : CurrentUser.Books
            };

            return View(viewmodel);
        }

        public ActionResult AddTo(string id = "")
        {
            BooksListViewModel viewModel = new BooksListViewModel();

            if(CurrentUser != null)
            {
                //Adding a book into user's books field like the user's cart
                CurrentUser.Books.Add(repository.Books.FirstOrDefault(b => b.Id == id));
                viewModel.Books = CurrentUser.Books;                
                repository.UpdateUser(CurrentUser);     //save back to mongodb           
            }
            return View(viewModel);
        }

        public ActionResult RemoveFrom(string id = "")
        {
            if (CurrentUser != null)
            {
                var item = CurrentUser.Books.FirstOrDefault(b => b.Id == id);
                bool re = CurrentUser.Books.Remove(item);
                repository.UpdateUser(CurrentUser); //update db data
            }
            return RedirectToAction("MyLib"); //update my library page simply
        }

        public ActionResult Register(string userName="", string password ="")
        {
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var user = repository.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
                if(user == null)
                {
                    user = new Models.User();
                    user.UserName = userName;
                    user.Password = password;
                    repository.UpdateUser(user);
                }
                else
                {
                    ModelState.AddModelError("", "User account already exists! ");
                    return View("", "");
                }
            }  
            else
            {
                ModelState.AddModelError("", "Enter the user name & password!");
                return View("", "");
            }      

            return RedirectToAction("Login",new { username = userName ,password = password });
        }


    }
}