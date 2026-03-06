using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Application.Processors;

/// <summary>
/// Classe abstrata que define o fluxo padrão de processamento de pedidos
/// usando o padrão Template Method. A ordem das etapas é imutável;
/// os detalhes de cada etapa são delegados à subclasse concreta.
/// </summary>
public abstract class PedidoProcessorBase
{
    /// <summary>
    /// Ponto de entrada público. Define e executa o fluxo completo na ordem correta.
    /// </summary>
    public void Processar(Pedido pedido)
    {
        ValidarPedido(pedido);

        var totalBruto    = CalcularTotalBruto(pedido);
        var totalDescontos = CalcularDescontos(pedido);
        var totalFinal    = CalcularTotalFinal(totalBruto, totalDescontos);

        RealizarPagamento(totalFinal);
        RegistrarLog(pedido, totalBruto, totalDescontos, totalFinal);
    }

    /// <summary>Valida o pedido antes de processá-lo.</summary>
    protected abstract void ValidarPedido(Pedido pedido);

    /// <summary>Retorna o total bruto (sem descontos) do pedido.</summary>
    protected abstract decimal CalcularTotalBruto(Pedido pedido);

    /// <summary>Retorna o valor total dos descontos a serem aplicados.</summary>
    protected abstract decimal CalcularDescontos(Pedido pedido);

    /// <summary>
    /// Calcula o valor final. Implementação padrão: bruto - descontos.
    /// Pode ser sobrescrita por subclasses que precisem de lógica diferente.
    /// </summary>
    protected virtual decimal CalcularTotalFinal(decimal totalBruto, decimal totalDescontos)
        => totalBruto - totalDescontos;

    /// <summary>Executa o pagamento do valor final calculado.</summary>
    protected abstract void RealizarPagamento(decimal valorFinal);

    /// <summary>Registra o resultado completo do processamento.</summary>
    protected abstract void RegistrarLog(
        Pedido pedido,
        decimal totalBruto,
        decimal totalDescontos,
        decimal totalFinal);
}
