using Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<IList<ClienteDto>> ObterTodosAsync();
        Task<ClienteDto> ObterPorIdAsync(int id);
        Task CadastrarAsync(ClienteDto dto);
        Task AtualizarAsync(int id, ClienteDto dto);
        Task RemoverAsync(int id);
    }
}
