using NHibernate;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using NHibernate.Linq;
using System.Threading.Tasks;
using Infrastructure.NHibernate;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public async Task AddAsync(Cliente cliente)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(cliente);
                    transaction.Commit();
                }
            }
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return await session.Query<Cliente>()
                    .Where(c => c.Id == id)
                    .FetchMany(c => c.Telefones)
                    .ToListAsync()
                    .ContinueWith(t => t.Result.GroupBy(c => c.Id)
                    .Select(g => g.First()).FirstOrDefault());
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(id);
                    transaction.Commit();
                }
            }
        }

        public async Task DeleteAsync(Cliente cliente)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(cliente);
                    transaction.Commit();
                }
            }
        }

        public async Task<IList<Cliente>> GetAllAsync()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return await session.Query<Cliente>().Fetch(t => t.Telefones).ToListAsync();
            }
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.UpdateAsync(cliente);
                    transaction.Commit();
                }
            }
        }
    }
}
