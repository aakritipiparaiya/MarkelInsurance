using Markel.com.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    public sealed class NewsLetterSubscriptionRepository : INewsLetterSubscriptionRepository
    {
        private readonly MarkelDbContext _dbContext;

        public NewsLetterSubscriptionRepository(MarkelDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        /// Add emails for NewsLetterSubscription
        /// </summary>
        public async Task<bool> Add(NewsLetterSubscription newsLetterSubscription)
        {
            try
            {
                if (newsLetterSubscription != null)
                {
                    await _dbContext.NewsLetterSubscriptions.AddAsync(newsLetterSubscription);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
