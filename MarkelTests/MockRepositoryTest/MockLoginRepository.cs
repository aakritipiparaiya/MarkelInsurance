using Markel.com.Controllers;
using Markel.com.Data;
using Markel.com.Data.MockRepository;
using Markel.com.Models;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkelTests.MockRepositoryTest
{
    public class MockLoginRepositoryTest
    {
        private MockLoginRepository _mockLoginRepo;

        [SetUp]
        public void Setup()
        {
            _mockLoginRepo = new MockLoginRepository();
        }

        [Test]
        public void SearchLogin_ValidIDPwd_LoginPassed()
        {
            string id = "aakriti";
            string pwd = "india";
            bool isExist = _mockLoginRepo.Search(new Markel.com.Models.Login() { Id = id, Pwd = pwd }).Result;
            Assert.IsTrue(isExist);
        }

        [Test]
        public void SearchLogin_InValidIDPwd_LoginFailed()
        {
            string id = "aakriti";
            string pwd = "wrong";
            bool isExist = _mockLoginRepo.Search(new Markel.com.Models.Login() { Id = id, Pwd = pwd }).Result;
            Assert.IsFalse(isExist);
        }

        [Test]
        public void SearchLogin_IdisNull_ThrowError()
        {
            Login login = new Login();
            login.Id = null;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _mockLoginRepo.Add(login));
            Assert.That(ex.Message, Is.EqualTo("ID cant be null"));
        }

        [Test]
        public void AddNewsLetterSubscription_PwdisNull_ThrowError()
        {
            Login login = new Login();
            login.Id = "aa";
            login.Pwd = null;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _mockLoginRepo.Add(login));
            Assert.That(ex.Message, Is.EqualTo("Pwd cant be null"));
        }

    }
}
