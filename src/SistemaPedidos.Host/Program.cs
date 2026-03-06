using Microsoft.Extensions.DependencyInjection;
using SistemaPedidos.Host.Extensions;
using SistemaPedidos.Application.Pagamentos;
using SistemaPedidos.Application.Processors;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;
using SistemaPedidos.Infrastructure.Logging;
using SistemaPedidos.Infrastructure.Repositories;
using SistemaPedidos.Presentation.Formatters;

PedidoResultFormatter.ExibirCabecalho();

var services = new ServiceCollection();

// Infraestrutura
services.AddSingleton<ILogger, ConsoleLogger>();
services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));

// ✨ BÔNUS — Reflection: descobre e registra automaticamente todas as classes
// que implementam IDesconto e possuem [DescontoConfig] no assembly Application.
// Para adicionar novo desconto: criar classe + decorar com [DescontoConfig(...)].
// Não é necessário alterar o Program.cs (OCP em ação).
var applicationAssembly = typeof(PedidoProcessor).Assembly;
services.AddDescontosViaReflection(applicationAssembly);

var formaPagamento = args.FirstOrDefault(a => a.StartsWith("--pagamento="))
                        ?.Split('=')[1]
                         .ToLowerInvariant()
                     ?? "pix";

if (formaPagamento == "cartao")
    services.AddSingleton<IPagamento, PagamentoCartao>();
else
    services.AddSingleton<IPagamento, PagamentoPix>();

services.AddTransient<PedidoProcessor>();
var provider  = services.BuildServiceProvider();
var processor = provider.GetRequiredService<PedidoProcessor>();

var mouse   = new Produto(1, "Mouse",   79.90m);
var teclado = new Produto(2, "Teclado", 159.90m);
var headset = new Produto(3, "Headset", 249.90m);
var repo = provider.GetRequiredService<IRepository<Produto>>();
repo.Add(mouse);
repo.Add(teclado);
repo.Add(headset);

PedidoResultFormatter.ExibirProdutos(repo.GetAll());

var pedido = new Pedido();
pedido.AdicionarItem(mouse,   2);  // Mouse   x2
pedido.AdicionarItem(teclado, 1);  // Teclado x1
pedido.AdicionarItem(headset, 2);  // Headset x2  → total: 5 itens (ativa DescontoPorQuantidade)

PedidoResultFormatter.ExibirItensDoPedido(pedido);

processor.Processar(pedido);