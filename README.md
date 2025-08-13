# GestaoEstagios API - Skeleton

Projeto skeleton para gerir pedidos de estágio.

**Como usar (local):**

1. Instale [.NET 7 SDK](https://dotnet.microsoft.com/download) e SQL Server Express + SSMS.
2. Crie a base de dados usando o script SQL que foi fornecido (ou use Migrations).
3. Ajuste `appsettings.json` com a connection string para o seu SQL Server Express.
4. No terminal, execute:

   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

5. A API estará disponível em `https://localhost:5001` por padrão.

**Migração para Azure:**
- Trocar connection string para Azure SQL.
- Usar Azure Blob Storage para ficheiros.
- Substituir autenticação por Azure AD.
- Deploy para Azure App Service.

Gerado: 2025-08-12T09:26:07.908002 UTC
