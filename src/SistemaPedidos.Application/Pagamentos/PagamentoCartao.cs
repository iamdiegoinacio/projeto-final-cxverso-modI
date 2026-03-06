using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Application.Pagamentos;

/// <summary>
/// Forma de pagamento via Cartão de Crédito. Implementação simples do contrato IPagamento.
/// </summary>
public class PagamentoCartao : IPagamento
{
    public string Nome => "Cartão de Crédito";

    public void Pagar(decimal valor)
    {
        // Em um sistema real, integraria com gateway de pagamento (ex.: Stripe, PagSeguro).
        Console.WriteLine($"  [Cartão] Processando cobrança de R$ {valor:F2}...");
        Console.WriteLine($"  [Cartão] Cartão aprovado. Autorização: {Guid.NewGuid().ToString()[..8].ToUpper()}");
        Console.WriteLine($"  [Cartão] Pagamento confirmado com sucesso!");
    }
}
