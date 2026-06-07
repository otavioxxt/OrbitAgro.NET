# OrbitAgro API - .NET

API REST do projeto OrbitAgro desenvolvida com ASP.NET Core e Oracle Database.

## Equipe
- Nickolas - RM564105
- Samara - RM566133
- Natália - RM564099
- Otávio - RM565960
- Rodrigo - RM565162

## Tecnologias
- ASP.NET Core
- Entity Framework Core
- Oracle Database
- Swagger/OpenAPI

## Como executar
1. Clone o repositório
2. Configure a string de conexão Oracle no `appsettings.json`
3. Execute `dotnet ef database update`
4. Execute `dotnet run`
5. Acesse `http://localhost:5004/swagger`

## Endpoints
- `/api/Produtor` - CRUD de produtores
- `/api/AreaCultivo` - CRUD de áreas de cultivo
- `/api/Monitoramento` - CRUD de monitoramentos
- `/api/Alerta` - CRUD de alertas

## Diagrama de Entidades
- TB_PRODUTOR 1:N TB_AREA_CULTIVO
- TB_AREA_CULTIVO 1:N TB_MONITORAMENTO
- TB_AREA_CULTIVO 1:N TB_ALERTA
