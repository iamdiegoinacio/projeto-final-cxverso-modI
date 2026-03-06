using SistemaPedidos.Domain.Attributes;
using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Application.Descontos;

/// <summary>
/// O atributo declara os argumentos do construtor para auto-registro via Reflection.
/// 0.10 = 10% de desconto.
/// </summary>
[DescontoConfig(0.10)]

/// <summary>
/// Desconto percentual sobre o total bruto do pedido.
/// O percentual é configurável via construtor, permitindo reutilização em diferentes cenários.
/// </summary>
public class DescontoPercentual : IDesconto
{
    private readonly decimal _percentual;

    /// <param name="percentual">Valor fracionário entre 0 e 1. Ex.: 0.10 para 10%.</param>
    public DescontoPercentual(decimal percentual)
    {
        if (percentual <= 0 || percentual >= 1)
            throw new ArgumentOutOfRangeException(nameof(percentual),
                "O percentual deve estar entre 0 e 1 (exclusivo).");

        _percentual = percentual;
    }

    public string Nome => $"DescontoPercentual ({_percentual * 100:F0}%)";

    public decimal CalcularDesconto(Pedido pedido)
        => pedido.TotalBruto() * _percentual;
}
