using Core.Enums;
using Core.Entities;
using FluentNHibernate.Mapping;

namespace Infrastructure.Mappings
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("Clientes");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Nome).Length(100);
            Map(x => x.Nome).Not.Nullable();

            Map(x => x.Sexo).Not.Nullable();
            Map(x => x.Sexo).CustomType<ESexo>();

            Map(x => x.Endereco).Nullable();

            HasMany(x => x.Telefones)
                .Cascade.AllDeleteOrphan()
                .Inverse().KeyColumn("ClienteId");
        }
    }
}
