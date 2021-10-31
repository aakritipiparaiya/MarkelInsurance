using Markel.com.Controllers;
using Markel.com.Data;
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
    public class MockNewsLetterSubscriptionRepositoryTest
    {
        private MockNewsLetterSubscriptionRepository _newsLetterSubscriptionRepository;

        [SetUp]
        public void Setup()
        {
            _newsLetterSubscriptionRepository = new MockNewsLetterSubscriptionRepository();
        }

        [Test]
        public void AddNewsLetterSubscription_ModelisNull_ThrowError()
        {
            NewsLetterSubscription news = null;
            
            Assert.Throws<ArgumentNullException>(() => _newsLetterSubscriptionRepository.Add(news));
        }

        [Test]
        public void AddNewsLetterSubscription_EmailAddressisNull_ThrowError()
        {
            NewsLetterSubscription news = new NewsLetterSubscription();
            news.EmailAddress = null;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _newsLetterSubscriptionRepository.Add(news));
            Assert.That(ex.Message, Is.EqualTo("Email Address is empty"));
        }

        [Test]
        public void AddNewsLetterSubscription_EmailAddressisInValid_ThrowError()
        {
            NewsLetterSubscription news = new NewsLetterSubscription();
            news.EmailAddress = "aaaa";

            Exception ex = Assert.Throws<Exception>(() => _newsLetterSubscriptionRepository.Add(news));
            Assert.That(ex.Message, Is.EqualTo("Email not valid"));
        }

        [Test]
        public void AddNewsLetterSubscription_AddEmailID_EmailSubscribed()
        {
            string email = "aakriti@gmail.com";
            _newsLetterSubscriptionRepository.Add(new Markel.com.Models.NewsLetterSubscription() { EmailAddress = email });
            Assert.IsTrue(_newsLetterSubscriptionRepository.AllEmails.Count(c => c == email) == 1);
        }

        [Test]
        public void AddNewsLetterSubscription_AddEmailIDTwice_EmailSubscribedOnlyOnce()
        {
            string email = "aakriti@gmail.com";
            _newsLetterSubscriptionRepository.Add(new Markel.com.Models.NewsLetterSubscription() { EmailAddress = email });
            Assert.IsTrue(_newsLetterSubscriptionRepository.AllEmails.Count(c => c == email) == 1);

            _newsLetterSubscriptionRepository.Add(new Markel.com.Models.NewsLetterSubscription() { EmailAddress = email });
            Assert.IsTrue(_newsLetterSubscriptionRepository.AllEmails.Count(c => c == email) == 1);
        }
    }
}
