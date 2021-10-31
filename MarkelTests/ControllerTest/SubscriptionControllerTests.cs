using Markel.com.Controllers;
using Markel.com.Data;
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
    public class SubscriptionControllerTests
    {
        private INewsLetterSubscriptionRepository _newsLetterSubscriptionRepository;
        private ILogger<SubscriptionController> _logger;

        [SetUp]
        public void Setup()
        {
            _newsLetterSubscriptionRepository = Substitute.For<INewsLetterSubscriptionRepository>();
            _logger = Substitute.For<ILogger<SubscriptionController>>();
        }

        [Test]
        public void WhenIndexActionIsCalled_ThenNewsLetterSubscriptionViewIsReturned()
        {
            var controller = new SubscriptionController(_logger, _newsLetterSubscriptionRepository);
            var result = controller.Index();
            Assert.IsInstanceOf(typeof(ViewResult), result);
            Assert.AreEqual(((ViewResult)result).ViewName, "NewsLetterSubscription");
        }

        [Test]
        public async Task WhenAddActionIsCalled_WithValidEmailAddress_ThenEmailAddressShouldBeAdded()
        {
            var controller = new SubscriptionController(_logger, _newsLetterSubscriptionRepository);
            const string emailAddress = "xyz@markel.com";
            var result = await controller.Add(new NewsLetterSubscription() { EmailAddress = emailAddress });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _newsLetterSubscriptionRepository.Received(1).Add(Arg.Is<NewsLetterSubscription>(x => x.EmailAddress == emailAddress));
        }

        [Test]
        public async Task WhenAddActionIsCalled_WithInvalidEmailAddress_ThenEmailAddressIsNotAdded()
        {
            var controller = new SubscriptionController(_logger, _newsLetterSubscriptionRepository);
            const string invalidEmailAddress = "xyz";
            controller.ModelState.AddModelError("email", "email is invalid");
            var result = await controller.Add(new NewsLetterSubscription() { EmailAddress = invalidEmailAddress });

            Assert.IsInstanceOf(typeof(ViewResult), result);
            await _newsLetterSubscriptionRepository.DidNotReceive().Add(Arg.Any<NewsLetterSubscription>());
        }
    }
}
