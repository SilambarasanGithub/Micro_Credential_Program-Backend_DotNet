using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Model;
using UserApi.Data.EFCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Constraints;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace UserApi.Tests
{
    class UserControllerTest
    {
        Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();
        List<User> users;
        private IUserRepository MockUserRepository;

        [SetUp]
        public void Setup()
        {
            users = new List<User> {
                new User { Id = 1, Name = "Rajesh", Passowrd="test123", Address="Dr.No 1343, SN Street, Palam", State = "Delhi", Country = "India", EmailAddress="david1991@gmail.com", ContactNumber="8976523442", DOB= Convert.ToDateTime("27/07/1991"), MobileNumber="8976523442"},
                new User { Id = 2, Name = "Sharma", Passowrd="test123", Address="No 19, Shaeed Chowk, Ranchi", State = "Jharkand", Country = "India", EmailAddress="sharma1987@gmail.com", ContactNumber="9897652442", DOB= Convert.ToDateTime("16/01/1987"), MobileNumber="9897652442"}
            };

            // Arrange Mock to Get All Users
            _mockUserRepository.Setup(m => m.GetUsers()).Returns(Task.FromResult<IEnumerable<User>>(users));

            // Arrange Mock to Get User By ID
            _mockUserRepository.Setup(m => m.GetUser(It.IsAny<int>()))
                .Returns((int i) => Task.FromResult(users.Where(x => x.Id == i).SingleOrDefault()));

            // Arrange Mock to Insert User
            _mockUserRepository.Setup(m => m.AddUser(It.IsAny<User>())).Returns(
                (User target) =>
                {
                    users.Add(target);
                    return Task.FromResult(target);
                });

            // Arrage Mock to Update User
            _mockUserRepository.Setup(m => m.UpdateUser(It.IsAny<User>())).Returns(
                (User target) =>
                {
                    var original = users.Where(x => x.Id == target.Id).Single();
                    if (original == null)
                    {
                        return null;
                    }
                    original.Id = target.Id;
                    original.Name = target.Name;
                    original.Passowrd = target.Passowrd;
                    original.Address = target.Address;
                    original.State = target.State;
                    original.Country = target.Country;
                    original.EmailAddress = target.EmailAddress;
                    original.ContactNumber = target.ContactNumber;
                    original.EmailAddress = target.EmailAddress;
                    original.MobileNumber = target.MobileNumber;
                    return Task.FromResult(target);
                });

            // Complete setup of Mock UserRepository
            MockUserRepository = _mockUserRepository.Object;
        }

        [Test]
        public void Test1_CanGetAllUsers()
        {
            // Act
            var allUsers = MockUserRepository.GetUsers();
            
            // Assert
            Assert.IsNotNull(allUsers);
            Assert.AreEqual(2, allUsers.Result.Count());
        }

        [Test]
        public void Test2_CanGetAllUserByID()
        {
            // Act
            User user = MockUserRepository.GetUser(2).Result;

            // Assert
            Assert.IsNotNull(user);
            Assert.IsInstanceOf(typeof(User), user);
            Assert.AreEqual("Sharma", user.Name);
        }

        [Test]
        public void Test3_CanAddNewUser()
        {
            // Arrange
            User newUser = new User { Id = 3, Name = "Madumitha", Passowrd = "test123", Address = " 34, Nandhi hills appartment, Banglore", State = "Karnataka", Country = "India", EmailAddress = "madumitha1984@gmail.com", ContactNumber = "653420090", DOB = Convert.ToDateTime("27/07/1991"), MobileNumber = "653420090" };

            //Act
            User user = MockUserRepository.AddUser(newUser).Result;

            // Assert
            Assert.AreEqual(3, MockUserRepository.GetUsers().Result.Count());
            Assert.IsNotNull(user);
            Assert.IsInstanceOf(typeof(User), user);
            Assert.AreEqual("Madumitha", user.Name);
        }

        [Test]
        public void Test4_CanUpdateUser()
        {
            // Arrange
            User user = MockUserRepository.GetUser(1).Result;
            user.Name = "Updated Name";

            //Act 
            var updatedUser = MockUserRepository.UpdateUser(user);

            //Assert
            Assert.AreEqual("Updated Name", updatedUser.Result.Name);
        }
    }
}
