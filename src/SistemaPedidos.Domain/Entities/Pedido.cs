namespace SistemaPedidos.Domain.Entities;

/// <summary>
/// Agregado raiz do domínio. Gerencia seus próprios itens (encapsulamento forte),
/// impedindo acesso externo mutável à coleção interna.
/// </summary>
public class Pedido
{
    private readonly List<ItemPedido> _itens = new();

    /// <summary>
    /// Lista de itens exposta de forma imutável (somente leitura).
    /// </summary>
    public IReadOnlyList<ItemPedido> Itens => _itens.AsReadOnly();

    /// <summary>
    /// Adiciona um item ao pedido. Garante quantidade mínima de 1.
    /// </summary>
    public void AdicionarItem(Produto produto, int quantidade)
    {
        var item = new ItemPedido(produto, quantidade);
        _itens.Add(item);
    }

    /// <summary>
    /// Calcula a soma bruta de todos os subtotais sem descontos.
    /// </summary>
    public decimal TotalBruto() => _itens.Sum(i => i.Subtotal());
}
