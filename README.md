# Teste Prático - Desenvolvedor .NET (OdontoClinic)

Este projeto foi desenvolvido como parte do processo seletivo para a vaga de Desenvolvedor .NET na OdontoClinic. O objetivo foi criar um sistema de cadastro de clientes, persistência de dados em SQL Server com NHibernate e cache de leitura utilizando Redis.

---

## 📌 Tecnologias Utilizadas

- ASP.NET MVC (.NET Framework 4.7.2)
- NHibernate + Fluent NHibernate
- SQL Server
- Redis (via StackExchange.Redis)
- DDD / Clean Architecture
- Unity (Injeção de Dependência)
- Docker (para Redis)

---

## ✅ Funcionalidades

- [x] Cadastro de clientes
- [x] Edição de clientes com validações
- [x] Exclusão e visualização de detalhes
- [x] Cada cliente pode ter múltiplos telefones, mas apenas um ativo
- [x] Redis para cache de leitura (cliente individual e listagem)
- [x] Arquitetura limpa com separação de responsabilidades

---

## 🗃️ Banco de Dados

- SQL Server local
- Geração de schema automática via NHibernate (FluentMappings)
- Connection string configurada em `Presentation/web.config`

---

## 🚀 Cache com Redis

- Leitura de cliente e listagem busca primeiro no Redis
- Caso não exista, busca no SQL Server e salva no Redis
- Cache limpo automaticamente em ações de edição e remoção
- Redis executado localmente via Docker e refêrenciado como localhost no CacheService:

```bash
docker run --name redis-dev -p 6379:6379 -d redis
```

--- 

## ▶️ Como executar o projeto

Tenha o SQL Server rodando localmente

Suba o Redis com Docker (veja acima)

Ajuste a connection string em web.config se necessário

Compile e execute a aplicação via Visual Studio

---

## 🔑 Dados de Exemplo

Na inicialização, um cliente é inserido automaticamente caso ainda não existam registros.

Cliente: Maria Souza
Telefone: 11988887777 (ativo)

---

## 👨‍💻 Desenvolvido por
**Marcelo Lima**
Desenvolvedor .NET Fullstack
[LinkedIn](https://www.linkedin.com/in/marcelolima11/)