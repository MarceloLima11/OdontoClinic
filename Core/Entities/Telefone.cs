namespace Core.Entities
{
    public class Telefone
    {
        public virtual int Id { get; protected set; }
        public virtual string Numero { get; protected set; }
        public virtual bool Ativo { get; protected set; }
        public virtual Cliente Cliente { get; protected set; }
    }
}
