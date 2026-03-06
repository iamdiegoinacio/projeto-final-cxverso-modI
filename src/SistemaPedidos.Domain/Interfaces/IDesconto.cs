using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Interfaces;

/// <summary>
/// Contrato (Strategy Pattern) para regras de desconto.
/// Novas regras de desconto devem implementar esta interface — sem alterar o processador (OCP).
/// </summary>
public interface IDesconto
{
    /// <summary>Nome descritivo do desconto, utilizado nos logs.</summary>
    string Nome { get; }

    /// <summary>Calcula o valor do desconto para o pedido fornecido.</summary>
    decimal CalcularDesconto(Pedido pedido);
}
