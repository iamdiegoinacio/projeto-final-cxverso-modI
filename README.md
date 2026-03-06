# Sistema de Pedidos e Pagamentos Extensível

Projeto final da disciplina **POO II**, implementado com **Clean Architecture**, **Clean Code** e boas práticas SOLID em C#.

---

## Estrutura da Solução

```
projeto final/
├── SistemaPedidos.sln
└── src/
    ├── SistemaPedidos.Domain/          ← Entidades + Interfaces (sem dependências externas)
    ├── SistemaPedidos.Application/     ← Lógica de negócio, descontos, pagamentos, processor
    ├── SistemaPedidos.Infrastructure/  ← Logger e repositório em memória
    ├── SistemaPedidos.Presentation/    ← Formatação e exibição no console
    └── SistemaPedidos.Host/            ← Composition Root (Program.cs)
```

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download) ou superior

---

## Como executar

### Pagamento via Pix (padrão)

```powershell
cd "c:\Users\iamdiegoinacio\OneDrive\Documentos\CXVERSO\projeto final"
dotnet run --project src/SistemaPedidos.Host
```

### Pagamento via Cartão

```powershell
dotnet run --project src/SistemaPedidos.Host -- --pagamento=cartao
```

---

## Saída esperada (Pix)

```
╔══════════════════════════════════════════╗
║   Sistema de Pedidos e Pagamentos v1.0   ║
║         POO II — Projeto Final           ║
╚══════════════════════════════════════════╝

  📦 Produtos cadastrados:
     [1] Mouse — R$ 79,90
     [2] Teclado — R$ 159,90
     [3] Headset — R$ 249,90

  🛒 Itens do pedido:
     Mouse x2 = R$ 159,80
     Teclado x1 = R$ 159,90
     Headset x2 = R$ 499,80
     Total de itens: 5

========================================
  RESUMO DO PEDIDO
========================================
  Total bruto:      R$ 819,50

  Descontos aplicados:
    - DescontoPercentual (10%): R$ 81,95
    - DescontoPorQuantidade (mín. 5 itens): R$ 20,00

  Total descontos:  R$ 101,95
  Total final:      R$ 717,55

  Pagamento: Pix
  Status: Pago com sucesso
========================================
```

---

## Princípios aplicados

| Princípio             | Como foi aplicado                                                       |
| --------------------- | ----------------------------------------------------------------------- |
| **SRP**               | Cada classe tem uma única responsabilidade                              |
| **OCP**               | Novos descontos/pagamentos: nova classe + registrar no DI               |
| **LSP**               | Todas as implementações de `IDesconto`/`IPagamento` são intercambiáveis |
| **ISP**               | Interfaces pequenas e coesas                                            |
| **DIP**               | Processor depende de abstrações, nunca de concretos                     |
| **Template Method**   | `PedidoProcessorBase` define o fluxo fixo de processamento              |
| **Strategy**          | `IDesconto` e `IPagamento` permitem trocar comportamento via DI         |
| **DI via Construtor** | `PedidoProcessor` recebe todas as dependências pelo construtor          |

---

## Como adicionar um novo desconto

1. Crie uma classe em `SistemaPedidos.Application/Descontos/` implementando `IDesconto`
2. Registre-a no container em `Program.cs`:
   ```csharp
   services.AddSingleton<IDesconto>(_ => new MeuNovoDesconto(...));
   ```
3. **Nenhuma outra alteração é necessária** (OCP em ação).
