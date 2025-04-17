using Core.Extensions;
using Core.Validations;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Cliente
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Sexo { get; protected set; }
        public virtual string Endereco { get; protected set; }
        public virtual List<Telefone> Telefones { get; protected set; } = new List<Telefone>();

        public Cliente(int id, string nome, string sexo, string endereco)
        {
            EntitieException.When(id < 0, "Valor de id inválido.");
            Id = id;

            ValidateCliente(nome, sexo, endereco);
        }

        private void ValidateCliente(string nome, string sexo, string endereco)
        {
            EntitieException.When(string.IsNullOrEmpty(nome), "O nome é obrigatório.");

            EntitieException.When(!sexo.IsSexoValid(), "Sexo inválido. Use Masculino, Feminino ou Outro.");

            Nome = nome;
            Sexo = sexo;
            Endereco = endereco;
        }

        public virtual void AdicionarTelefone(Telefone telefone)
        {
            if (telefone.Ativo)
            {
                EntitieException.When(Telefones.Exists(t => t.Ativo), "Já existe um telefone ativo.");
            }

            Telefones.Add(telefone);
        }
    }
}
