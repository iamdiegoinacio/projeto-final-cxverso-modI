using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Application.Processors;

/// <summary>
/// Implementação concreta do processador de pedidos.
/// Recebe todas as dependências via injeção de construtor (DIP).
/// Nunca instancia descontos ou pagamentos diretamente — sem new, sem if/switch.
/// </summary>
public class PedidoProcessor : PedidoProcessorBase
{
    private readonly IEnumerable<IDesconto> _descontos;
    private readonly IPagamento _pagamento;
    private readonly ILogger _logger;

    public PedidoProcessor(
        IEnumerable<IDesconto> descontos,
        IPagamento pagamento,
        ILogger logger)
    {
        _descontos = descontos ?? throw new ArgumentNullException(nameof(descontos));
        _pagamento = pagamento ?? throw new ArgumentNullException(nameof(pagamento));
        _logger    = logger    ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override void ValidarPedido(Pedido pedido)
    {
        if (pedido is null)
            throw new ArgumentNullException(nameof(pedido), "O pedido não pode ser nulo.");

        if (!pedido.Itens.Any())
            throw new InvalidOperationException("O pedido deve conter ao menos um item.");
    }

    protected override decimal CalcularTotalBruto(Pedido pedido)
        => pedido.TotalBruto();

    /// <summary>
    /// Aplica todos os descontos registrados via polimorfismo — sem if/switch.
    /// O Strategy Pattern resolve qual desconto efetivamente aplica.
    /// </summary>
    protected override decimal CalcularDescontos(Pedido pedido)
        => _descontos.Sum(d => d.CalcularDesconto(pedido));

    protected override void RealizarPagamento(decimal valorFinal)
        => _pagamento.Pagar(valorFinal);

    protected override void RegistrarLog(
        Pedido pedido,
        decimal totalBruto,
        decimal totalDescontos,
        decimal totalFinal)
    {
        _logger.Info($"");
        _logger.Info($"========================================");
        _logger.Info($"  RESUMO DO PEDIDO");
        _logger.Info($"========================================");
        _logger.Info($"  Total bruto:      R$ {totalBruto:F2}");
        _logger.Info($"");
        _logger.Info($"  Descontos aplicados:");

        foreach (var desconto in _descontos)
        {
            var valorDesconto = desconto.CalcularDesconto(pedido);
            _logger.Info($"    - {desconto.Nome}: R$ {valorDesconto:F2}");
        }

        _logger.Info($"");
        _logger.Info($"  Total descontos:  R$ {totalDescontos:F2}");
        _logger.Info($"  Total final:      R$ {totalFinal:F2}");
        _logger.Info($"");
        _logger.Info($"  Pagamento: {_pagamento.Nome}");
        _logger.Info($"  Status: Pago com sucesso");
        _logger.Info($"========================================");
    }
}
