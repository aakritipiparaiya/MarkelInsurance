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
    public class MockContactFormRepositoryTest
    {
        private MockContactFormRepository _mockContactFormRepository;

        [SetUp]
        public void Setup()
        {
            _mockContactFormRepository = new MockContactFormRepository();
        }

        [Test]
        public void AddContactForm_ModelisNull_ThrowError()
        {
            ContactForm form = null;

            Assert.Throws<ArgumentNullException>(() => _mockContactFormRepository.Add(form));
        }

        [Test]
        public void AddContactForm_EmailAddressisNull_ThrowError()
        {
            ContactForm form = new ContactForm();
            form.EmailAddress = null;

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _mockContactFormRepository.Add(form));
            Assert.That(ex.Message, Is.EqualTo("Email Address is empty"));
        }

        [Test]
        public void AddContactForm_EmailAddressisInValid_ThrowError()
        {
            ContactForm contactForm = new ContactForm();
            contactForm.EmailAddress = "aaaa";

            Exception ex = Assert.Throws<Exception>(() => _mockContactFormRepository.Add(contactForm));
            Assert.That(ex.Message, Is.EqualTo("Email not valid"));
        }

        [Test]
        public async Task AddContactForm_ValidContactForm_ContactFormAdded()
        {
            string email = "aakriti@gmail.com";
            await _mockContactFormRepository.Add(new Markel.com.Models.ContactForm() { EmailAddress = email, Description = "test", Subject = "test" });

            var forms = await _mockContactFormRepository.GetAllForms();
            Assert.IsTrue(forms.Any(c => c.EmailAddress == email));

        }

        [Test]
        public void AddContactForm_InValidContactForm_ContactFormNotAdded()
        {
            string email = string.Empty;
            Assert.Throws<ArgumentException>(() => _mockContactFormRepository.Add(new Markel.com.Models.ContactForm() { EmailAddress = email, Description = "test", Subject = "test" }));
                
        }
    }
}
