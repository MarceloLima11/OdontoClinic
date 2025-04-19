using Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IList<ClienteDto>> ObterTodos();
        Task<ClienteDto> ObterPorId(int id);
        void Cadastrar(ClienteDto dto);
        void Atualizar(int id, ClienteDto dto);
        void Remover(int id);
    }
}
