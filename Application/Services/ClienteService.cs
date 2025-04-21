using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Application.DTOs;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task CadastrarAsync(ClienteDto dto)
        {
            Cliente cliente = new Cliente(dto.Nome, dto.Sexo, dto.Endereco);

            foreach (string tel in dto.Telefones)
            {
                Telefone telefone = new Telefone(tel, true);
                cliente.AdicionarTelefone(telefone);
            }

            await _clienteRepository.AddAsync(cliente);
        }

        public async Task<ClienteDto> ObterPorIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente is null) 
                return null;

            var clienteDto = new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Sexo = cliente.Sexo.ToString(),
                Endereco = cliente.Endereco,
                Telefone = cliente.Telefones.FirstOrDefault(x => x.Ativo).Numero,
                Telefones = cliente.Telefones.Select(x => x.Numero).ToList()
            };

            return clienteDto;
        }

        public async Task<IList<ClienteDto>> ObterTodosAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            if (clientes is null)
                return new List<ClienteDto>();

            var clientesDto = clientes.Select(x => new ClienteDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Sexo = x.Sexo.ToString(),
                Endereco = x.Endereco,
                Telefone = x.Telefones.FirstOrDefault(t => t.Ativo).Numero,
                Telefones = x.Telefones.Select(t => t.Numero).ToList()
            }).ToList();

            return clientesDto;
        }

        public async Task AtualizarAsync(int id, ClienteDto dto)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if(cliente is null)
                throw new Exception("Cliente não encontrado.");

            cliente.AlterarDados(dto.Nome, dto.Sexo, dto.Endereco);

            cliente.Telefones.Clear();
            foreach (var numero in dto.Telefones)
            {
                cliente.AdicionarTelefone(new Telefone(numero, true));
            }

            if(!string.IsNullOrWhiteSpace(dto.Telefone))
            {
                var novoTelefone = new Telefone(dto.Telefone, true)
                {
                    Cliente = cliente
                };
                cliente.AdicionarTelefone(novoTelefone);
            }

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task RemoverAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente is null)
                throw new Exception("Cliente não encontrado.");

            await _clienteRepository.DeleteAsync(cliente);
        }
    }
}
