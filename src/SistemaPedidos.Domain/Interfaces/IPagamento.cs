namespace SistemaPedidos.Domain.Interfaces;

/// <summary>
/// Contrato (Strategy Pattern) para formas de pagamento.
/// Novas formas de pagamento devem implementar esta interface — sem alterar o processador (OCP).
/// </summary>
public interface IPagamento
{
    /// <summary>Nome descritivo da forma de pagamento, utilizado nos logs.</summary>
    string Nome { get; }

    /// <summary>Executa o pagamento do valor informado.</summary>
    void Pagar(decimal valor);
}
