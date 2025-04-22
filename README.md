# 🦷 Teste Prático - Desenvolvedor .NET (OdontoClinic)

Este projeto foi desenvolvido como parte do processo seletivo para a vaga de Desenvolvedor .NET na **OdontoClinic**. O objetivo foi criar um sistema de **cadastro de clientes**, com persistência em **SQL Server** usando **NHibernate** e cache de leitura com **Redis**.

---

## 🛠️ Tecnologias Utilizadas

- ASP.NET MVC (.NET Framework 4.7.2)
- NHibernate + Fluent NHibernate
- SQL Server
- Redis (StackExchange.Redis)
- Docker (Redis)
- Unity (Injeção de Dependência)
- DDD / Clean Architecture

---

## ✅ Funcionalidades Implementadas

- Cadastro, edição, exclusão e visualização de clientes
- Cada cliente pode ter múltiplos telefones, com **apenas um ativo**
- Formulário dinâmico de telefones com validações em JavaScript
- Validações de domínio e regras de negócio
- Cache de leitura com Redis (detalhes e listagem)
- Arquitetura limpa com separação clara entre camadas:
  - `Core`, `Application`, `Infrastructure`, `Presentation`

---

## 📃 Banco de Dados

- Banco: **SQL Server Local**
- Geração de schema automática via **Fluent NHibernate**
- Connection string configurada em:\
  `Presentation/web.config`

### 🧪 Criar banco local (caso necessário)

```sql
CREATE DATABASE ClientesDb;
```

---

## 🧱 Geração do Banco de Dados

O projeto utiliza o recurso `SchemaUpdate` do NHibernate para **gerar as tabelas automaticamente** ao rodar a aplicação, desde que o banco de dados `ClientesDb` já exista no SQL Server.

> ⚠️ Atenção: O método `SchemaExport`, que recria o schema inteiro, **apaga todos os dados** existentes. Por isso, ele foi substituído por `SchemaUpdate`, que apenas cria o que estiver faltando.

Caso queira forçar a recriação do schema (útil nos primeiros testes), basta descomentar o trecho:

```csharp
.ExposeConfiguration(config => new SchemaExport(config).Create(false, true))


---

## 🧠 Redis Cache

- Implementação via `StackExchange.Redis`
- Busca de cliente verifica cache primeiro (read-through)
- Cache atualizado após criação/edição e removido na exclusão

### Subir Redis via Docker:

```bash
docker run --name redis-dev -p 6379:6379 -d redis
```

---

## ▶️ Executando o Projeto

1. Instale o SQL Server e crie o banco conforme acima
2. Suba o Redis via Docker (ou local)
3. Ajuste a string de conexão se necessário
4. Abra a solução no Visual Studio 2019
5. Compile e execute o projeto

---

## 🥪 Dados de Exemplo

Na primeira execução, se o banco estiver vazio, um cliente é inserido automaticamente:

- **Nome:** Maria Souza
- **Telefone:** 11988887777 (ativo)

---

## ❗ Tratamento de Erros

- Erros HTTP como 404 são redirecionados para views customizadas
- Validações de domínio são exibidas ao usuário no formulário

---

## 👨‍💼 Desenvolvido por

**Marcelo Lima**\
Desenvolvedor .NET Fullstack\
[LinkedIn →](https://www.linkedin.com/in/marcelolima11/)

---

## ✅ Pontos Extras

- 🛡️ Uso de DDD com foco em boas práticas
- 🧠 Camadas bem definidas e desacopladas
- 📂 Redis funcionando em Docker + cache inteligente
- 💅 Layout estilizado com Bootstrap + ícones
- 📄 README explicativo + script para setup

---

📝 Obs: O projeto já está configurado para gerar as tabelas automaticamente via NHibernate (`SchemaExport`). Basta garantir que o banco `ClientesDb` exista no seu SQL Server local. 

Foi um super desafio, obrigado pela oportunidade! 😎

