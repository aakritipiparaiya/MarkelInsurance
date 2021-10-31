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
    public class ContactFormControllerTest
    {
        private IContactFormRepository _contactFormRepository;
        private ILogger<ContactFormController> _logger;

        [SetUp]
        public void Setup()
        {
            _contactFormRepository = Substitute.For<IContactFormRepository>();
            _logger = Substitute.For<ILogger<ContactFormController>>();
        }

        [Test]
        public void WhenIndexActionIsCalled_ThenContactFormViewIsReturned()
        {
            var controller = new ContactFormController(_logger, _contactFormRepository);
            var result = controller.Index();
            Assert.IsInstanceOf(typeof(ViewResult), result);
            Assert.AreEqual(((ViewResult)result).ViewName, "ContactForm");
        }

        [Test]
        public async Task WhenAddActionIsCalled_WithValidEmail_ThenContactFormShouldBeAdded()
        {
            var controller = new ContactFormController(_logger, _contactFormRepository);
            const string email = "aak@gmail.com";
            const string pwd = "pwd";
            var result = await controller.Add(new ContactForm() { EmailAddress = email,Subject = "ww" });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _contactFormRepository.Received(1).Add(Arg.Is<ContactForm>(x => x.EmailAddress == email));
        }

        public async Task WhenAddActionIsCalled_WithInValidEmail_ThenContactFormShouldNotBeAdded()
        {
            var controller = new ContactFormController(_logger, _contactFormRepository);
            const string email = "aakgmail.com";
            const string pwd = "pwd";
            controller.ModelState.AddModelError("email", "email not in correct format");
            var result = await controller.Add(new ContactForm() { EmailAddress = email, Subject = "ww" });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _contactFormRepository.DidNotReceive().Add(Arg.Any<ContactForm>());
        }

        [Test]
        public async Task WhenAddActionIsCalled_WithInvalidSubject_ThenContactIsNotAdded()
        {
            var controller = new ContactFormController(_logger, _contactFormRepository);
            const string email = "aak@gmail.com";
            const string sub = "abcdefghijklmnlklklkljlhlkhlgkljjkkkkkkkgkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk";           
            controller.ModelState.AddModelError("sub", "subject cant be more than 50 chars");
            var result = await controller.Add(new ContactForm() { EmailAddress = email, Subject = sub });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _contactFormRepository.DidNotReceive().Add(Arg.Any<ContactForm>());
        }
    }
}
