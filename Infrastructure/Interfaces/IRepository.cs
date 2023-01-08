using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IList<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
