using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core.Interfaces.Base
{
    public interface IRepositoryBase<G> where G : class
    {
        Task<G> GetById(int id);
        void Delete(int id);
        void Delete(G entity);
        void Add(G entity);
        void Update(G entity);
        Task<IList<G>> GetAll();
    }
}
