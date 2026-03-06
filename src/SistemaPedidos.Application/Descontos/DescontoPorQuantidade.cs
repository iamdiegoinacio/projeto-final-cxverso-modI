using SistemaPedidos.Domain.Attributes;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Application.Descontos;

/// <summary>
/// O atributo declara os argumentos do construtor para auto-registro via Reflection.
/// 20.0 = R$ 20 de desconto fixo, 5 = quantidade mínima de itens.
/// </summary>
[DescontoConfig(20.0, 5)]

/// <summary>
/// Desconto de valor fixo aplicado quando a quantidade total de itens do pedido
/// atingir ou ultrapassar o limite mínimo configurado.
/// </summary>
public class DescontoPorQuantidade : IDesconto
{
    private readonly decimal _valorFixo;
    private readonly int _quantidadeMinima;

    /// <param name="valorFixo">Valor fixo a ser descontado quando a condição for atingida.</param>
    /// <param name="quantidadeMinima">Quantidade mínima total de itens para ativar o desconto. Padrão: 5.</param>
    public DescontoPorQuantidade(decimal valorFixo, int quantidadeMinima = 5)
    {
        if (valorFixo <= 0)
            throw new ArgumentOutOfRangeException(nameof(valorFixo), "O valor do desconto deve ser positivo.");

        _valorFixo = valorFixo;
        _quantidadeMinima = quantidadeMinima;
    }

    public string Nome => $"DescontoPorQuantidade (mín. {_quantidadeMinima} itens)";

    public decimal CalcularDesconto(Pedido pedido)
    {
        var totalItens = pedido.Itens.Sum(i => i.Quantidade);
        return totalItens >= _quantidadeMinima ? _valorFixo : 0m;
    }
}
