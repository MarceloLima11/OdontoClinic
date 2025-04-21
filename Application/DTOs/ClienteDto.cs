using System.Collections.Generic;

namespace Application.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public List<string> Telefones { get; set; } = new List<string>();
    }
}
