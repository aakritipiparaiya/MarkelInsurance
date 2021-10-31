using Markel.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data.Interface
{
    public interface ILoginRepository
    {
        Task<bool> Add(Login login);
        Task<bool> Search(Login login);
    }
}
