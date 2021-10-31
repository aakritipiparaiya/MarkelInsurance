using Markel.com.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    /// <summary>
    /// Mock News Letter Subscription Database
    /// </summary>
    public class MockNewsLetterSubscriptionRepository : INewsLetterSubscriptionRepository
    {
        private readonly HashSet<string> emailAddresses = new HashSet<string>();

        public List<string> AllEmails
        {
            get { return emailAddresses.ToList(); }
        }

        /// <summary>
        /// Add an email for subscription
        /// </summary>

        public Task<bool> Add(NewsLetterSubscription newsLetterSubscription)
        {
            try
            {
                if (newsLetterSubscription == null)
                    throw new ArgumentNullException(nameof(newsLetterSubscription));

                if (string.IsNullOrWhiteSpace(newsLetterSubscription.EmailAddress))
                    throw new ArgumentException("Email Address is empty");
                
                if(!ValidateEmail(newsLetterSubscription.EmailAddress))
                    throw new Exception("Email not valid");

                //Check if email already subscribed return false so that duplicate msg can be displayed
                if(emailAddresses.Contains(newsLetterSubscription.EmailAddress))
                    return Task.FromResult(false);

                emailAddresses.Add(newsLetterSubscription.EmailAddress);

                return Task.FromResult(true);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Validate Email format
        /// </summary>
        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success) return true;
            return false;
        }

    }
}
