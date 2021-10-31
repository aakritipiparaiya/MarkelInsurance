using Markel.com.Controllers;
using Markel.com.Data;
using Markel.com.Data.Interface;
using Markel.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkelTests.ControllerTests
{
    public class LoginControllerTest
    {
        private ILoginRepository _loginRepository;
        private ILogger<LoginController> _logger;

        [SetUp]
        public void Setup()
        {
            _loginRepository = Substitute.For<ILoginRepository>();
            _logger = Substitute.For<ILogger<LoginController>>();
        }

        [Test]
        public void WhenIndexActionIsCalled_ThenLoginViewIsReturned()
        {
            var controller = new LoginController(_logger, _loginRepository);
            var result = controller.Index();
            Assert.IsInstanceOf(typeof(ViewResult), result);
            Assert.AreEqual(((ViewResult)result).ViewName, "Login");
        }

        [Test]
        public async Task WhenSearchActionIsCalled_WithValidIdPwd_ThenLoginIsSearched()
        {
            var controller = new LoginController(_logger, _loginRepository);
            const string id = "id";
            const string pwd = "pwd";
            var result = await controller.Search(new Login() { Id = id,Pwd=pwd });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _loginRepository.Received(1).Search(Arg.Is<Login>(x => x.Id == id && x.Pwd == pwd));
        }

        [Test]
        public async Task WhenSearchActionIsCalled_WithInvalidpwd_ThenLoginIsNotSearched()
        {
            var controller = new LoginController(_logger, _loginRepository);
            const string id = "xyz";
            const string pwd = "";
            controller.ModelState.AddModelError("pwd", "pwd can not be empty");
            var result = await controller.Search(new Login() { Id = id, Pwd = pwd });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _loginRepository.DidNotReceive().Add(Arg.Any<Login>());
        }
    }
}
