using Core.Validations;

namespace Core.Entities
{
    public class Telefone
    {
        public virtual int Id { get; protected set; }
        public virtual string Numero { get; protected set; }
        public virtual bool Ativo { get; protected set; }
        public virtual Cliente Cliente { get; protected set; }

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

        public void Desativar() => Ativo = false;

        private void ValidateTelefone(string numero, bool ativo)
        {
            EntitieException.When(string.IsNullOrWhiteSpace(numero), "O número de telefone é obrigatório.");

            Numero = numero;
            Ativo = ativo;
        }
    }
}
