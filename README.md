# 🌱 OrbitAgro API — .NET

> Monitoramento Agrícola via Satélite e IoT — FIAP Global Solution 2026/1

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Oracle](https://img.shields.io/badge/Oracle-F80000?style=for-the-badge&logo=oracle&logoColor=white)
![Railway](https://img.shields.io/badge/Railway-0B0D0E?style=for-the-badge&logo=railway&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

API REST desenvolvida em ASP.NET Core com Oracle Database, parte do ecossistema OrbitAgro que conecta dados de satélites e sensores IoT para monitoramento agrícola em tempo real.

---

## 👥 Equipe

| Nome | RM | Turma |
|---|---|---|
| Nickolas Davi | RM564105 | 2TDSR |
| Samara Vilela de Oliveira | RM566133 | 2TDSR |
| Natália Cristina de Souza | RM564099 | 2TDSR |
| Otávio Ferreira | RM565960 | 2TDSR |
| Rodrigo Carvalho Silva | RM565162 | 2TDSR |

---

## 🔗 Links

| Item | Link |
|---|---|
| 🚀 Deploy | https://orbitagronet-production.up.railway.app/swagger |
| 📦 Repositório | https://github.com/otavioxxt/OrbitAgro.NET |

---

## 💡 Sobre o Projeto

Produtores rurais perdem safras porque percebem problemas visualmente, quando já é tarde demais. O **OrbitAgro** combina monitoramento por satélite e sensores IoT para dar visão em tempo real da saúde da lavoura. O produtor vê áreas no mapa, acompanha o NDVI, recebe alertas inteligentes e decide mais rápido para reduzir perdas.

**ODS Atendidos:** ODS 2 (Fome Zero) · ODS 9 (Inovação) · ODS 13 (Ação Climática)

---

## 🏗️ Arquitetura da Solução

```
┌─────────────────────┐     HTTP/REST      ┌─────────────────────────┐
│  📱 App Mobile       │ ──────────────────▶│                         │
│  React Native        │                    │   🔷 API .NET (Railway) │
└─────────────────────┘                    │                         │
                                            │   ASP.NET Core          │
┌─────────────────────┐     HTTP/REST      │   Entity Framework      │
│  📡 ESP32 IoT        │ ──────────────────▶│   Swagger UI            │
│  Wokwi               │                    │                         │
└─────────────────────┘                    └────────────┬────────────┘
                                                         │
                                                   Oracle EF Core
                                                         │
                                            ┌────────────▼────────────┐
                                            │  🗄️ Oracle Database      │
                                            │  FIAP                    │
                                            └─────────────────────────┘
```

---

## 🗄️ Diagrama de Entidades

```
TB_PRODUTOR
├── Id (PK)
├── Nome
├── Email
├── Telefone
├── Cpf
└── DataCadastro
        │
        │ 1:N
        ▼
TB_AREA_CULTIVO
├── Id (PK)
├── NomeArea
├── Cultura
├── Latitude
├── Longitude
├── Hectares
└── ProdutorId (FK)
        │
        ├─────────────────────────────────┐
        │ 1:N                             │ 1:N
        ▼                                 ▼
TB_MONITORAMENTO                    TB_ALERTA
├── Id (PK)                         ├── Id (PK)
├── IndiceNdvi                      ├── TipoAlerta
├── NdviAnterior                    ├── Observacao
├── UmidadeSolo                     ├── DataAlerta
├── TemperaturaSolo                 ├── StatusAlerta
├── DataLeitura                     └── AreaCultivoId (FK)
├── AreaCultivoId (FK)
└── FonteSateliteId (FK)
        ▲
        │ 1:N
T_TB_FONTE_SATELITE
├── Id (PK)
├── NomeFonte
└── Ativo

T_TB_LOG_ERRO
├── Id (PK)
├── NomeProcedure
├── NomeUsuario
├── DataHoraErro
├── CodigoErro
└── Mensagem
```

---

## 🔷 Regras de Negócio

| Regra | Condição | Resultado |
|---|---|---|
| RN01 | NDVI < 0,55 | Gerar alerta automático |
| RN02 | NDVI ≥ 0,55 | 🟢 Área Saudável |
| RN03 | NDVI ≥ 0,40 e < 0,55 | 🟡 Área em Atenção |
| RN04 | NDVI < 0,40 | 🔴 Área Crítica |
| RN05 | Umidade solo < 25% | ⚠️ Risco de Seca |
| RN06 | Temperatura solo > 30°C | 🌡️ Estresse por Calor |

---

## 🛠️ Tecnologias

| Tecnologia | Uso |
|---|---|
| ASP.NET Core | Framework principal da API |
| Entity Framework Core | ORM para acesso ao banco |
| Oracle Database | Banco de dados relacional |
| Swashbuckle (Swagger) | Documentação e testes da API |
| Railway | Deploy em nuvem |
| EF Core Migrations | Versionamento do banco |

---

## 📁 Estrutura do Projeto

```
OrbitAgro.API/
├── Controllers/
│   ├── ProdutorController.cs
│   ├── AreaCultivoController.cs
│   ├── MonitoramentoController.cs
│   ├── AlertaController.cs
│   ├── FonteSateliteController.cs
│   └── LogErroController.cs
├── Data/
│   └── ApplicationContext.cs
├── Models/
│   ├── ProdutorEntity.cs
│   ├── AreaCultivoEntity.cs
│   ├── MonitoramentoEntity.cs
│   ├── AlertaEntity.cs
│   ├── FonteSateliteEntity.cs
│   └── LogErroEntity.cs
├── Migrations/
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```

---

## ⚙️ Como Executar

```bash
# 1. Clone o repositório
git clone https://github.com/otavioxxt/OrbitAgro.NET

# 2. Configure a string de conexão no appsettings.Development.json
"ConnectionStrings": {
  "Oracle": "Data Source=...;User Id=...;Password=..."
}

# 3. Execute as migrations
dotnet ef database update

# 4. Execute o projeto
dotnet run

# 5. Acesse o Swagger
http://localhost:5004/swagger
```

---

## 📡 Endpoints

### Produtor
| Método | Endpoint | Descrição | Status |
|---|---|---|---|
| GET | `/api/Produtor` | Lista todos os produtores | 200 / 204 |
| GET | `/api/Produtor/{id}` | Busca produtor por ID | 200 / 404 |
| POST | `/api/Produtor` | Cadastra novo produtor | 200 / 400 |
| PUT | `/api/Produtor/{id}` | Atualiza produtor | 200 / 404 |
| DELETE | `/api/Produtor/{id}` | Remove produtor | 204 / 404 |

### Área de Cultivo
| Método | Endpoint | Descrição | Status |
|---|---|---|---|
| GET | `/api/AreaCultivo` | Lista todas as áreas | 200 / 204 |
| GET | `/api/AreaCultivo/{id}` | Busca área por ID | 200 / 404 |
| POST | `/api/AreaCultivo/produtor/{produtorId}` | Cadastra área para produtor | 200 / 404 |
| PUT | `/api/AreaCultivo/{id}` | Atualiza área | 200 / 404 |
| DELETE | `/api/AreaCultivo/{id}` | Remove área | 204 / 404 |

### Monitoramento
| Método | Endpoint | Descrição | Status |
|---|---|---|---|
| GET | `/api/Monitoramento` | Lista todos os monitoramentos | 200 / 204 |
| GET | `/api/Monitoramento/{id}` | Busca monitoramento por ID | 200 / 404 |
| POST | `/api/Monitoramento/area/{areaId}` | Cadastra monitoramento para área | 200 / 404 |
| PUT | `/api/Monitoramento/{id}` | Atualiza monitoramento | 200 / 404 |
| DELETE | `/api/Monitoramento/{id}` | Remove monitoramento | 204 / 404 |

### Alerta
| Método | Endpoint | Descrição | Status |
|---|---|---|---|
| GET | `/api/Alerta` | Lista todos os alertas | 200 / 204 |
| GET | `/api/Alerta/{id}` | Busca alerta por ID | 200 / 404 |
| POST | `/api/Alerta/area/{areaId}` | Cadastra alerta para área | 200 / 404 |
| PUT | `/api/Alerta/{id}` | Atualiza alerta | 200 / 404 |
| DELETE | `/api/Alerta/{id}` | Remove alerta | 204 / 404 |

### Fonte Satélite
| Método | Endpoint | Descrição | Status |
|---|---|---|---|
| GET | `/api/FonteSatelite` | Lista todas as fontes | 200 / 204 |
| GET | `/api/FonteSatelite/{id}` | Busca fonte por ID | 200 / 404 |
| POST | `/api/FonteSatelite` | Cadastra nova fonte | 200 / 400 |
| PUT | `/api/FonteSatelite/{id}` | Atualiza fonte | 200 / 404 |
| DELETE | `/api/FonteSatelite/{id}` | Remove fonte | 204 / 404 |

### Log de Erro
| Método | Endpoint | Descrição | Status |
|---|---|---|---|
| GET | `/api/LogErro` | Lista todos os logs | 200 / 204 |
| GET | `/api/LogErro/{id}` | Busca log por ID | 200 / 404 |
| POST | `/api/LogErro` | Registra novo log | 200 / 400 |
| DELETE | `/api/LogErro/{id}` | Remove log | 204 / 404 |

---

## 🧪 Testes

### Fluxo Completo de Teste

#### 1️⃣ Cadastrar Produtor
```http
POST https://orbitagronet-production.up.railway.app/api/Produtor
Content-Type: application/json

{
  "nome": "João da Silva",
  "email": "joao@fazenda.com",
  "telefone": "11999999999",
  "cpf": "123.456.789-00"
}
```
✅ **Resposta 200 OK**
```json
{
  "id": 1,
  "nome": "João da Silva",
  "email": "joao@fazenda.com",
  "telefone": "11999999999",
  "cpf": "123.456.789-00",
  "dataCadastro": "2026-06-09T00:00:00",
  "areas": null
}
```

---

#### 2️⃣ Cadastrar Área de Cultivo
```http
POST https://orbitagronet-production.up.railway.app/api/AreaCultivo/produtor/1
Content-Type: application/json

{
  "nomeArea": "Talhão Norte",
  "cultura": "Soja",
  "latitude": -23.5,
  "longitude": -46.6,
  "hectares": 150
}
```
✅ **Resposta 200 OK**
```json
{
  "id": 1,
  "nomeArea": "Talhão Norte",
  "cultura": "Soja",
  "latitude": -23.5,
  "longitude": -46.6,
  "hectares": 150,
  "produtorId": 1
}
```

---

#### 3️⃣ Cadastrar Monitoramento
```http
POST https://orbitagronet-production.up.railway.app/api/Monitoramento/area/1
Content-Type: application/json

{
  "indiceNdvi": 0.45,
  "ndviAnterior": 0.60,
  "umidadeSolo": 22.0,
  "temperaturaSolo": 31.5
}
```
✅ **Resposta 200 OK**
```json
{
  "id": 1,
  "indiceNdvi": 0.45,
  "ndviAnterior": 0.60,
  "umidadeSolo": 22.0,
  "temperaturaSolo": 31.5,
  "dataLeitura": "2026-06-09T00:00:00",
  "areaCultivoId": 1
}
```
> ⚠️ NDVI 0.45 → 🟡 Atenção (RN03) | Umidade 22% → Risco de Seca (RN05) | Temp 31.5°C → Estresse Calor (RN06)

---

#### 4️⃣ Cadastrar Alerta
```http
POST https://orbitagronet-production.up.railway.app/api/Alerta/area/1
Content-Type: application/json

{
  "tipoAlerta": "Seca",
  "observacao": "Umidade abaixo de 25% — risco de seca confirmado",
  "statusAlerta": "Ativo"
}
```
✅ **Resposta 200 OK**

---

#### 5️⃣ Cadastrar Fonte de Satélite
```http
POST https://orbitagronet-production.up.railway.app/api/FonteSatelite
Content-Type: application/json

{
  "nomeFonte": "Sentinel-2",
  "ativo": true
}
```
✅ **Resposta 200 OK**

---

### ❌ Casos de Erro

#### Produtor não encontrado
```http
GET /api/Produtor/999
```
❌ **Resposta 404 Not Found**
```json
{ "mensagem": "Produtor não encontrado." }
```

#### Área não encontrada ao criar monitoramento
```http
POST /api/Monitoramento/area/999
```
❌ **Resposta 404 Not Found**
```json
{ "mensagem": "Área de cultivo não encontrada." }
```

---

## 🔄 Relacionamentos

```
TB_PRODUTOR (1) ──────────────────── (N) TB_AREA_CULTIVO
TB_AREA_CULTIVO (1) ──────────────── (N) TB_MONITORAMENTO
TB_AREA_CULTIVO (1) ──────────────── (N) TB_ALERTA
T_TB_FONTE_SATELITE (1) ──────────── (N) TB_MONITORAMENTO
```
