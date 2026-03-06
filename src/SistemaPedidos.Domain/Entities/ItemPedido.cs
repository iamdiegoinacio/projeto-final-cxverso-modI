namespace SistemaPedidos.Domain.Entities;

/// <summary>
/// Entidade de domínio representando um item dentro de um pedido.
/// Encapsula produto e quantidade, com validação e cálculo de subtotal.
/// </summary>
public class ItemPedido
{
    public Produto Produto { get; }
    public int Quantidade { get; }

    public ItemPedido(Produto produto, int quantidade)
    {
        ArgumentNullException.ThrowIfNull(produto);

        if (quantidade < 1)
            throw new ArgumentException("A quantidade deve ser no mínimo 1.", nameof(quantidade));

        Produto = produto;
        Quantidade = quantidade;
    }

    /// <summary>
    /// Calcula o valor total do item (Preço × Quantidade).
    /// </summary>
    public decimal Subtotal() => Produto.Preco * Quantidade;
}
