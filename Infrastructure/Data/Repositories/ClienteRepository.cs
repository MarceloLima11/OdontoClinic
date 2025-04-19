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
        public void Add(Cliente cliente)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(cliente);
                    transaction.Commit();
                }
            }
        }

        public async Task<Cliente> GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return await session.GetAsync<Cliente>(id);
            }
        }

        public void Delete(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.DeleteAsync(id);
                    transaction.Commit();
                }
            }
        }

        public void Delete(Cliente cliente)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(cliente);
                    transaction.Commit();
                }
            }
        }

        public async Task<IList<Cliente>> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return await session.Query<Cliente>().ToListAsync();
            }
        }

        public void Update(Cliente cliente)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(cliente);
                    transaction.Commit();
                }
            }
        }
    }
}
