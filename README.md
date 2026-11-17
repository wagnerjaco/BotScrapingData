# BotScrapingData | .NET 10 + Selenium + PostgreSQL

Aplicação de automação desenvolvida em **C#/.NET 10**, utilizando **Selenium WebDriver** para executar processos automatizados em sistemas web e integrar dados ao PostgreSQL.

O projeto implementa um fluxo de automação do sistema Sienge, desde a navegação e obtenção de arquivos até o processamento e restauração de dados.

## Funcionalidades

- Automação de login e navegação com Selenium WebDriver.
- Execução de operações automatizadas no sistema Sienge.
- Identificação e processamento de arquivos compactados GZIP.
- Restauração de backups PostgreSQL via `pg_restore`.
- Orquestração das etapas de automação.
- Logging estruturado e tratamento de exceções.

## Stack Tecnológica

**Linguagem:** C# / .NET 10  
**Automação:** Selenium WebDriver, ChromeDriver  
**Banco de dados:** PostgreSQL  
**Logging:** Serilog  
**Configuração:** appsettings.json  
**Arquitetura:** Domain, Application, Infrastructure e Console

## Diferenciais Técnicos

- Separação entre orquestração, automação web e infraestrutura.
- Uso de interfaces e injeção de dependência.
- Processamento de arquivos com `System.IO.Compression`.
- Integração com ferramentas externas por execução de processos.
- Rastreabilidade da execução por logs.

**Objetivo:** demonstrar habilidades em automação de processos empresariais, integração de dados, manipulação de arquivos e desenvolvimento de aplicações .NET com responsabilidades bem definidas.
