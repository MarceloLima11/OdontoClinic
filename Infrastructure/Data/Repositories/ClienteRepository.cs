using NHibernate;
using Core.Entities;
using NHibernate.Linq;
using Core.Interfaces;
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
                return await session.Query<Cliente>().Fetch(x => x.Telefones).FirstOrDefaultAsync(x => x.Id == id);
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
