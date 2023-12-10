using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Repository;

namespace Vzeeta.Core.Service
{
    public interface ICustomService<T>
    {
        
        Task<T> FindById(Expression<Func<T, bool>> match, string[] includes = null);
       
    }
}
