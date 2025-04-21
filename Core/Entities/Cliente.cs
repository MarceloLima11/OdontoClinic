using System;
using Core.Enums;
using Core.Extensions;
using Core.Validations;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Cliente
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual ESexo Sexo { get; protected set; }
        public virtual string Endereco { get; protected set; }
        public virtual IList<Telefone> Telefones { get; protected set; } = new List<Telefone>();

        protected Cliente() {}

        public Cliente(int id, string nome, string sexo, string endereco)
        {
            EntitieException.When(id < 0, "Valor de id inválido.");
            Id = id;

            ValidateCliente(nome, sexo, endereco);
        }

        public Cliente(string nome, string sexo, string endereco)
        {
            ValidateCliente(nome, sexo, endereco);
        }

        public virtual void AlterarDados(string nome, string sexo, string endereco) => ValidateCliente(nome, sexo, endereco);

        public virtual void AdicionarTelefone(Telefone telefone)
        {
            if (telefone.Ativo)
            {
                foreach (Telefone tel in Telefones)
                    tel.Desativar();
            }

            telefone.Cliente = this;
            Telefones.Add(telefone);
        }

        private void ValidateCliente(string nome, string sexo, string endereco)
        {
            EntitieException.When(string.IsNullOrEmpty(nome), "O nome é obrigatório.");

            EntitieException.When(!sexo.IsSexoValid(), "Sexo inválido. Use Masculino, Feminino ou Outro.");

            Nome = nome;
            Sexo = (ESexo)Enum.Parse(typeof(ESexo), sexo.Trim(), ignoreCase: true);
            Endereco = endereco;
        }
    }
}
