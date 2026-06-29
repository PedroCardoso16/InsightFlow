# InsightFlow

O **InsightFlow** é um sistema web para gestão de demandas por perfilO **InsightFlow** é um sistema web para gestão de demandas com dashboard analítico, desenvolvido com foco em demonstrar conhecimentos em desenvolvimento Full Stack utilizando **Blazor WebAssembly no Frontend** e **.NET com C# no Backend**.
- Deploy de aplicação web

---

## Tecnologias Utilizadas

### Backend

- .NET
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Swagger / OpenAPI

### Frontend

- Blazor WebAssembly
- HTML
- CSS
- Bootstrap
- HttpClient
- LocalStorage para armazenamento do token JWT

### Banco de Dados

- SQL Server
- Entity Framework Core
- Migrations
- Seed automático de dados iniciais

### Versionamento

- Git
- Git Flow
- GitHub

### Deploy

- API publicada em hospedagem ASP.NET/.NET
- Frontend Blazor WebAssembly publicado em hospedagem web
- Banco de dados SQL Server online

---

## Arquitetura do Projeto

O projeto foi organizado em camadas para separar responsabilidades:

```text
InsightFlow
├── InsightFlow.Api
├── InsightFlow.Application
├── InsightFlow.Domain
├── InsightFlow.Infrastructure
└── InsightFlow.Web
```

### InsightFlow.Api

Camada responsável pela exposição dos endpoints REST, configuração de autenticação, autorização, Swagger, CORS e controllers da aplicação.

### InsightFlow.Application

Camada responsável pelos DTOs, serviços, interfaces e regras de aplicação.

### InsightFlow.Domain

Camada responsável pelas entidades principais e enums do domínio.

### InsightFlow.Infrastructure

Camada responsável pelo acesso ao banco de dados, `DbContext`, migrations e repositories.

### InsightFlow.Web

Camada de frontend desenvolvida em Blazor WebAssembly, responsável pela interface do usuário, navegação, autenticação no client e consumo da API.

---

## Funcionalidades Implementadas

## Autenticação

- Login com e-mail e senha
- Geração de token JWT
- Armazenamento do token no LocalStorage
- Envio automático do token nas requisições HTTP
- Logout
- Proteção de rotas no Blazor

---

## Autorização

O sistema possui controle de acesso baseado em perfis.

| Perfil | Permissões |
|---|---|
| Admin | Acesso completo ao sistema |
| Analyst | Acesso a categorias, demandas e dashboard |
| User | Acesso a demandas e dashboard |

---

## Perfis de Acesso

### Admin

```json
{
  "email": "admin@insightflow.com",
  "password": "Admin@123"
}
```

Permissões:

- Gerenciar usuários
- Gerenciar categorias
- Gerenciar demandas
- Visualizar dashboard
- Acessar endpoints administrativos

---

### Analyst

```json
{
  "email": "analista@insightflow.com",
  "password": "Analista@123"
}
```

Permissões:

- Gerenciar categorias
- Gerenciar demandas
- Visualizar dashboard
- Não possui permissão para gerenciar usuários

---

### User

```json
{
  "email": "usuario@insightflow.com",
  "password": "Usuario@123"
}
```

Permissões:

- Visualizar dashboard
- Visualizar demandas
- Criar demandas conforme regra atual
- Não possui permissão para gerenciar usuários
- Não possui permissão para gerenciar categorias

---

## Módulos do Sistema

## Dashboard

O dashboard apresenta indicadores analíticos como:

- Total de demandas
- Demandas abertas
- Demandas em andamento
- Demandas concluídas
- Demandas canceladas
- Taxa de conclusão
- Tempo médio de resolução
- Demandas por status
- Percentual por prioridade
- Top categorias
- Evolução mensal

---

## Demandas

Funcionalidades:

- Listar demandas
- Criar demandas
- Alterar status da demanda
- Visualizar prioridade
- Visualizar categoria
- Visualizar solicitante
- Visualizar responsável
- Visualizar data de criação

Status disponíveis:

```text
1 = Aberta
2 = Em andamento
3 = Concluída
4 = Cancelada
```

Prioridades disponíveis:

```text
1 = Baixa
2 = Média
3 = Alta
4 = Crítica
```

---

## Categorias

Funcionalidades:

- Listar categorias
- Criar categorias
- Editar categorias
- Inativar categorias

---

## Usuários

Funcionalidades:

- Listar usuários
- Criar usuários
- Editar usuários
- Inativar usuários

---

## Endpoints Principais

## Auth

```http
POST /api/Auth/login
```

---

## Dashboard

```http
GET /api/Dashboard/summary
GET /api/Dashboard/by-status
GET /api/Dashboard/by-priority
GET /api/Dashboard/by-category
GET /api/Dashboard/monthly-evolution
GET /api/Dashboard/priority-percentages
GET /api/Dashboard/top-categories
GET /api/Dashboard/average-resolution-time
```

---

## Demandas

```http
GET /api/Demands
POST /api/Demands
GET /api/Demands/{id}
PUT /api/Demands/{id}
PATCH /api/Demands/{id}/status
DELETE /api/Demands/{id}
```

---

## Categorias

```http
GET /api/Categories
POST /api/Categories
GET /api/Categories/{id}
PUT /api/Categories/{id}
DELETE /api/Categories/{id}
```

---

## Usuários

```http
GET /api/Users
POST /api/Users
GET /api/Users/{id}
PUT /api/Users/{id}
DELETE /api/Users/{id}
```

---

## Como Executar o Projeto Localmente

### Pré-requisitos

- Visual Studio
- .NET SDK
- SQL Server LocalDB ou SQL Server
- Git

---

## Configuração do Banco Local

A connection string utilizada em ambiente local está no arquivo:

```text
InsightFlow.Api/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=InsightFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## Executar Migrations

Na raiz do projeto, execute:

```bash
dotnet ef database update --project InsightFlow.Infrastructure --startup-project InsightFlow.Api --context InsightFlow.Infrastructure.Data.AppDbContext
```

---

## Executar Backend

Execute o projeto:

```text
InsightFlow.Api
```

A API estará disponível em uma URL semelhante a:

```text
https://localhost:7098
```

O Swagger pode ser acessado em:

```text
https://localhost:7098/swagger
```

---

## Executar Frontend

Execute o projeto:

```text
InsightFlow.Web
```

Acesse:

```text
/login
```

---

## Fluxo de Uso

1. Fazer login
2. Acessar o dashboard
3. Consultar demandas
4. Criar nova demanda
5. Alterar status da demanda
6. Visualizar indicadores atualizados no dashboard
7. Gerenciar categorias
8. Gerenciar usuários com perfil Admin

---

## Seed de Dados

O projeto possui seed automático para criação de dados iniciais.

Ao iniciar a API em um banco vazio, o sistema cria:

- Usuário Admin
- Usuário Analyst
- Usuário User
- Categorias iniciais
- Demandas de exemplo

Isso facilita a demonstração do sistema e permite testar o dashboard logo após a publicação.

---

## Observação sobre Segurança

Este projeto possui credenciais de demonstração para facilitar testes em ambiente de avaliação.

Em um ambiente real de produção, seria recomendado:

- Trocar as senhas padrão
- Armazenar connection strings em variáveis de ambiente
- Utilizar secrets seguros
- Configurar CORS de forma restrita
- Implementar refresh token
- Adicionar logs e auditoria

---

## Destaques Técnicos

- Arquitetura em camadas
- Uso de DTOs
- Uso de Services e Repositories
- API REST documentada com Swagger
- Autenticação JWT
- Autorização por perfil
- Blazor consumindo API protegida
- Dashboard orientado a dados
- Soft delete em categorias e usuários
- Versionamento com Git
- Publicação online da aplicação
- Seed automático de dados iniciais

---

## Melhorias Futuras

- Melhorar responsividade da interface
- Adicionar gráficos visuais ao dashboard
- Criar paginação nas listagens
- Criar filtros avançados no frontend
- Adicionar logs de auditoria
- Implementar refresh token
- Criar testes automatizados
- Melhorar tratamento global de erros
- Criar pipeline CI/CD
- Aplicar controle visual por perfil no frontend

---

## Autor

Desenvolvido por **Pedro da Costa Cardoso**.

Projeto criado para demonstrar conhecimentos em desenvolvimento web com:

- Blazor WebAssembly
- .NET
- C#
- API REST
- SQL Server
- Entity Framework Core
- JWT
- Swagger
- Bootstrap
- Git Flow
- Arquitetura em camadas

O projeto foi criado para apresentar uma solução organizada, funcional e orientada a dados, permitindo cadastrar, acompanhar e analisar demandas por meio de indicadores visuais e operacionais.

---

## URLs do Projeto

### Frontend

```text
http://insightflow-web.runasp.net/login
```

### API

```text
http://insightflow-api.runasp.net
```

### Swagger

```text
http://insightflow-api.runasp.net/swagger
```

---

## Status do Projeto

Projeto publicado e funcional em ambiente online.

Funcionalidades já implementadas:

- Backend em .NET com API REST
- Frontend em Blazor WebAssembly
- Autenticação com JWT
- Autorização por perfil
- CRUD de demandas
- CRUD de categorias
- CRUD de usuários
- Dashboard analítico
- Consumo de API protegida pelo frontend
- Proteção de rotas no Blazor
- Seed automático de dados iniciais
- Publicação online da API e do Frontend
- Versionamento com Git e GitHub

Melhorias futuras:

- Melhorias visuais e responsividade avançada
- Controle visual de menus conforme perfil do usuário
- Paginação em listagens
- Filtros avançados no frontend
- Gráficos visuais no dashboard
- Logs de auditoria
- Testes automatizados
- Pipeline CI/CD

---

## Objetivo do Projeto

O objetivo do **InsightFlow** é permitir o controle de demandas internas, categorizando solicitações, acompanhando status, prioridades, responsáveis e gerando indicadores para apoio à tomada de decisão.

O sistema foi pensado para demonstrar conhecimentos em:

- Desenvolvimento Backend com .NET e C#
- Desenvolvimento Frontend com Blazor WebAssembly
- API REST
- Autenticação e autorização com JWT
- Consumo de API no Frontend
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Git Flow
- Organização em camadas
- Dashboard analítico
