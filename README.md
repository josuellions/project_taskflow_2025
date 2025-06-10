
 ![2025-06_taskflow_swagger_api_](https://github.com/user-attachments/assets/4cbae206-9130-4f1f-a706-31503839ddec)

 ### Processo seletivo vaga Developer

##### Data: 03/06/2025 a 08/06/2025

##### Developer: Josuel A. Lopes

##### About

Desenvolvimento de uma API ASP.NET Core 8 C# que permite aos usuários organizar e monitorar suas tarefas diárias, bem como colaborares e equipe.

- ASP.NET Core 8 C#,
- Microsoft.EntityFrameworkCore.Sqlite,
- Entity Framework,
- FluentAssert,
- Swagger,
- Bogus,
- Moq

<br/>

#### Projeto: TaskFlow

</br>

#### 📋 Sumário

---

- [📋 Sumário](#-sumário)
- [📂 Arquitetura e diretórios](#-arquitetura-e-diretórios)
- [📦 Pacotes](#-pacotes)
- [🧰 Dependências](#-dependências)
- [♻️ Variáveis de Ambiente](#-variáveis-de-ambiente)
- [🔥 Como executar](#-como-executar)
- [🧪 Testes](#-testes)
- [📜 Sugestões](#-sugestões)
- [💡 Melhorias](#-version)

<br/>

#### 📂 Arquitetura e diretórios

---

- MVC (Model View Controller)

```txt
  📦 root
  ┣ 📂 src
  ┃ ┗ 📂 taskflow.API (Projeto API)
  ┣ 📂 tests
  ┃ ┗ 📂 UseCases.Test (Projeto TESTES)
  ┣ 📜 .dockerignore
  ┣ 📜 docker-compose.yml
  ┣ 📜 Dockerfile.js
  ┗ 📜 taskflow.sln

```

<br/>

#### 📦 Pacotes

---

- Versão do ASP.NET Core

- - `Core 8`

- Padronização do código

- - Configurações
- - - `docker-compose.yml`
- - - `Program.cs` (Ajustar caminho do banco dados na linha 48)

<br/>

#### 🧰 Dependências

---

- Docker
- - Docker Compose
- - - Criar e inicializar

```bash
docker compose up --build -d
docker ps
```

- Banco Dados

- - SQlite (DBMS - Banco Dados relacional)

- - - Criar Banco Dados 

- - - Criar as Tabelas conforme exemplo query no arquivo `DataBase\Commons\Querys\20250608_create_tables.sql`

<br/>

#### ♻️ Variáveis de Ambiente

---

- Certifique-se de ter configurado o arquivo `Program.cs` na raiz do projeto `taskflow.API`, com as variáveis de ambiente necessárias para execução do projeto.

- Caso você não tenha acesso aos valores, solicite ao responsável pelo projeto.

<br/>

#### 🔥 Como executar

---

- Realize o clone ou baixe o projeto localmente.

- - Instalar ou atualizar os pacotes e dependências com gerenciador de pacotes

```
NuGet
```

- - Para executar o projeto certifique de ter instalado `dotnet`.
	- Acesse o diretorio do projeto  `taskflow.API`

```bash
dotnet --version
dotnet run
```

<br/>

#### 🧪 Testes

---

- Teste Automatizados / Teste Integração

- - TDD (Test Driven Development)

- - Para executar o projeto em modo de test acesse projeto `tests\UseCases.Test`.

```bash
dotnet test
```

ou

```bash
dotnet test -v n
```

<br/>

#### 📜 Sugestões

---

- Refinamentos - Duvidas PO:

- - Existem categorias ou tipos de tarefas que precisam de regras mais especifica
- - As tarefas podem depender uma das outras
- - Como o sistema deve tratar as tarefas vencidas


#### 💡 Melhorias

---

- Sugestões Desenvolvedor:

- - Implementar funcionalidade de envio de notificações por email na criação, edição ou exclusão das tarefas para os envolvidos no projeto.
- - Gerar relatório em Excel ou PDF para analise dos gestores
- - Implementar um CI/CD
