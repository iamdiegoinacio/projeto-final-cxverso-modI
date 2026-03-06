using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Presentation.Formatters;

/// <summary>
/// Responsável por formatar e exibir informações do pedido no console.
/// Centraliza a lógica de apresentação, mantendo o Host e o Processor livres de lógica de UI.
/// </summary>
public static class PedidoResultFormatter
{
    /// <summary>
    /// Exibe um cabeçalho de boas-vindas ao sistema.
    /// </summary>
    public static void ExibirCabecalho()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║   Sistema de Pedidos e Pagamentos v1.0   ║");
        Console.WriteLine("║         POO II — Projeto Final           ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
    }

    /// <summary>
    /// Exibe os detalhes dos produtos que serão usados no pedido.
    /// </summary>
    public static void ExibirProdutos(IEnumerable<Produto> produtos)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("  📦 Produtos cadastrados:");
        foreach (var p in produtos)
            Console.WriteLine($"     [{p.Id}] {p.Nome} — R$ {p.Preco:F2}");
        Console.ResetColor();
    }

    /// <summary>
    /// Exibe os itens que foram adicionados ao pedido antes do processamento.
    /// </summary>
    public static void ExibirItensDoPedido(Pedido pedido)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("  🛒 Itens do pedido:");
        foreach (var item in pedido.Itens)
            Console.WriteLine($"     {item.Produto.Nome} x{item.Quantidade} = R$ {item.Subtotal():F2}");

        var totalItens = pedido.Itens.Sum(i => i.Quantidade);
        Console.WriteLine($"     Total de itens: {totalItens}");
        Console.ResetColor();
        Console.WriteLine();
    }
}
