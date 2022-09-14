using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReaAccountingSys.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}