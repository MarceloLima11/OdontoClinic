using Core.Enums;
using NHibernate;
using System.Linq;
using Core.Entities;

namespace Infrastructure.Data.Seed
{
    public class DatabaseSeeder
    {
        public static void Popular(ISession session)
        {
            var bancoPopulado = session.Query<Cliente>().Any();
            if (bancoPopulado)
                return;

            using (var transaction = session.BeginTransaction())
            {
                Cliente cliente = new Cliente("Maria Souza", ESexo.Feminino.ToString(), "Av. Brasil, 1000");
                Telefone telefone = new Telefone("11988887777", true);

                cliente.AdicionarTelefone(telefone);

                session.Save(cliente);

                transaction.Commit();
            }
        }
    }
}
