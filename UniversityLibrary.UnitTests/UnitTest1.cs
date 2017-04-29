using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UniversityLibrary.WebUI.Models;
using UniversityLibrary.WebUI.Controllers;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UniversityLibrary.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<ILibraryRepository> mock = new Mock<ILibraryRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
                {
                    new Book { Title="Title1",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title2",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title3",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title4",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title5",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title6",Publisher="Pub1",Description="Desc1" }
                }.AsQueryable()
            );
            LibraryController con = new LibraryController(mock.Object);
            con.PageSize = 2;           
            //Action
            BooksListViewModel result = (BooksListViewModel)con.List(1).Model;
            //Assert
            Book[] bookArray = result.Books.ToArray();
            Assert.IsTrue(bookArray.Length == 2);
            Assert.AreEqual(bookArray[0].Title , "Title1");
            Assert.AreEqual(bookArray[1].Title , "Title2");
        }

        [TestMethod]
        public void Can_Send_PageInfo()
        {
            //Arrange
            Mock<ILibraryRepository> mock = new Mock<ILibraryRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
                {
                    new Book { Title="Title1",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title2",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title3",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title4",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title5",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title6",Publisher="Pub1",Description="Desc1" }
                }.AsQueryable()
            );
            LibraryController con = new LibraryController(mock.Object);
            con.PageSize = 3;
            //Action
            BooksListViewModel result = (BooksListViewModel)con.List(2).Model;
            //Assert
            PagingInfo pageinfo = result.PagingInfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 6);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_SearchBooks()
        {
            //Arrange
            Mock<ILibraryRepository> mock = new Mock<ILibraryRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
                {
                    new Book { Title="Title1",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title2",Publisher="Pub1",Description="Desc1" },
                    new Book { Title="Title3",Publisher="Pub2",Description="Desc1" },
                    new Book { Title="Title4",Publisher="Pub2",Description="Desc1" },
                    new Book { Title="Title5",Publisher="Pub3",Description="Desc1" },
                    new Book { Title="Title6",Publisher="Pub3",Description="Desc1" }
                }.AsQueryable()
            );
            LibraryController con = new LibraryController(mock.Object);
            con.PageSize = 3;
            //Action
            BooksListViewModel result = (BooksListViewModel)con.SearchResult("Pub2").Model;

            //Assert            
            Assert.AreEqual(result.Books.Count(), 2);
            Assert.AreEqual(result.Books.ElementAt(0).Title, "Title3");
            Assert.AreEqual(result.Books.ElementAt(1).Title, "Title4");            

        }

        [TestMethod]
        public void Can_Login()
        {
            //Arrange
            Mock<ILibraryRepository> mock = new Mock<ILibraryRepository>();
            MongoRepository.MongoRepository<User> repository = new MongoRepository.MongoRepository<User>();                        
            mock.Setup(m => m.Users).Returns(repository.ToList().AsQueryable());                        
            LibraryController con = new LibraryController(mock.Object);
            //Action
            User temp = new User();
            temp.UserName = "yhson";
            temp.Password = "1111";
            repository.Update(temp);            
            LoginViewModel lvw = (LoginViewModel)con.Login("yhson", "1111").Model;
            //Assert            
            Assert.AreEqual(lvw.User.UserName, "yhson");
            Assert.AreEqual(lvw.User.Password, "1111");
        }
    }
}
