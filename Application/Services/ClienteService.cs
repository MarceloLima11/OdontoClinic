using System;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Application.DTOs;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ICacheService _cacheService;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository, ICacheService cacheService)
        {
            _cacheService = cacheService;
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

            await _cacheService.RemoveAsync("clientes:todos");

            await _clienteRepository.AddAsync(cliente);
        }

        public async Task<ClienteDto> ObterPorIdAsync(int id)
        {
            var cacheKey = $"cliente:{id}";
            var cachedJson = await _cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedJson))
            {
                return JsonConvert.DeserializeObject<ClienteDto>(cachedJson);
            }

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

            var json = JsonConvert.SerializeObject(clienteDto);
            await _cacheService.SetAsync(cacheKey, json);

            return clienteDto;
        }

        public async Task<IList<ClienteDto>> ObterTodosAsync()
        {
            const string cacheKey = "clientes:todos";

            var cachedJson = await _cacheService.GetAsync(cacheKey);
            if(!string.IsNullOrEmpty(cachedJson))
            {
                return JsonConvert.DeserializeObject<IList<ClienteDto>>(cachedJson);
            }

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

            var json = JsonConvert.SerializeObject(clientesDto);
            await _cacheService.SetAsync(cacheKey, json);

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

            await _cacheService.RemoveAsync("clientes:todos");
            await _cacheService.RemoveAsync($"cliente:{id}");

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task RemoverAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente is null)
                throw new Exception("Cliente não encontrado.");

            await _cacheService.RemoveAsync("clientes:todos");
            await _cacheService.RemoveAsync($"cliente:{id}");

            await _clienteRepository.DeleteAsync(cliente);
        }
    }
}
