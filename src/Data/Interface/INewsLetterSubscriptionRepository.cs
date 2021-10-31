using Markel.com.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    public interface INewsLetterSubscriptionRepository
    {
        Task<bool> Add(NewsLetterSubscription newsLetterSubscription);
    }
}
