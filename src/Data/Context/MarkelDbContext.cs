using Markel.com.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    public class MarkelDbContext: DbContext
    {
        public MarkelDbContext(DbContextOptions<MarkelDbContext> options)
            :base(options)
        {
        }

        public DbSet<NewsLetterSubscription> NewsLetterSubscriptions { get; set; }

        public DbSet<ContactForm> ContactForms { get; set; }

        public DbSet<Login> Login { get; set; }
    }
}
