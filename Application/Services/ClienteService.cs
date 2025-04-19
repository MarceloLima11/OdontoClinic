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

        public void Cadastrar(ClienteDto dto)
        {
            Cliente cliente = new Cliente(dto.Nome, dto.Sexo, dto.Endereco);

            foreach (string tel in dto.Telefones)
            {
                Telefone telefone = new Telefone(tel, true);
                cliente.AdicionarTelefone(telefone);
            }

            _clienteRepository.Add(cliente);
        }

        public async Task<ClienteDto> ObterPorId(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            if (cliente is null) 
                return null;

            var clienteDto = new ClienteDto
            {
                Nome = cliente.Nome,
                Sexo = cliente.Sexo.ToString(),
                Endereco = cliente.Endereco,
                Telefones = cliente.Telefones.Select(x => x.Numero).ToList()
            };

            return clienteDto;
        }

        public async Task<IList<ClienteDto>> ObterTodos()
        {
            var clientes = await _clienteRepository.GetAll();
            if (clientes is null)
                return null;

            var clientesDto = clientes.Select(x => new ClienteDto
            {
                Nome = x.Nome,
                Sexo = x.Sexo.ToString(),
                Endereco = x.Endereco,
                Telefones = x.Telefones.Select(t => t.Numero).ToList()
            }).ToList();

            return clientesDto;
        }

        public async void Atualizar(int id, ClienteDto dto)
        {
            var cliente = await _clienteRepository.GetById(id);
            if(cliente is null)
                throw new Exception("Cliente não encontrado.");

            cliente.AlterarDados(dto.Nome, dto.Sexo, dto.Endereco);

            cliente.Telefones.Clear();
            foreach (var numero in dto.Telefones)
            {
                cliente.AdicionarTelefone(new Telefone(numero, true));
            }

            _clienteRepository.Update(cliente);
        }

        public async void Remover(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            if (cliente is null)
                throw new Exception("Cliente não encontrado.");

            _clienteRepository.Delete(cliente);
        }
    }
}
