using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Domain.Services
{
   public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
