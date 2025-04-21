using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core.Interfaces.Base
{
    public interface IRepositoryBase<G> where G : class
    {
        Task<G> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task DeleteAsync(G entity);
        Task AddAsync(G entity);
        Task UpdateAsync(G entity);
        Task<IList<G>> GetAllAsync();
    }
}
