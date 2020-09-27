using ContactManagement.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.Model
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(DataTablesHelper dataTables);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        int Count();
    }
}
