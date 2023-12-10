using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.Repository
{
    public interface IRepository<T,D> where T : class
    {
        IEnumerable<T> GetAll(int page,int pageSize,Func<T,bool> query, string[] includes=null);
        IEnumerable<T> GetAllEntities();
        Task<T> GetById(D id, string[] includes=null);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(D id);
    }
}
