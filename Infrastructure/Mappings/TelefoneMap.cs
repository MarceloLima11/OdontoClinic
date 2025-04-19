using Core.Entities;
using FluentNHibernate.Mapping;

namespace Infrastructure.Mappings
{
    public class TelefoneMap : ClassMap<Telefone>
    {
        public TelefoneMap()
        {
            Table("Telefone");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Numero).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.Cliente)
                .Column("ClienteId")
                .Not.Nullable();
        }
    }
}
