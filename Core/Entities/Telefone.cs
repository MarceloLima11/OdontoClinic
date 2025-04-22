using Core.Validations;

namespace Core.Entities
{
    public class Telefone
    {
        public virtual int Id { get; protected set; }
        public virtual string Numero { get; protected set; }
        public virtual bool Ativo { get; protected set; }
        public virtual Cliente Cliente { get; set; }

        protected Telefone() {}

        public Telefone(int id, string numero, bool ativo)
        {
            EntitieException.When(id < 0, "Valor de id inválido.");
            Id = id;

            ValidateTelefone(numero, ativo);
        }

        public Telefone(string numero, bool ativo)
        {
            ValidateTelefone(numero, ativo);
        }

        public virtual void Desativar() => Ativo = false;

        private void ValidateTelefone(string numero, bool ativo)
        {
            Numero = numero;
            Ativo = ativo;
        }
    }
}
