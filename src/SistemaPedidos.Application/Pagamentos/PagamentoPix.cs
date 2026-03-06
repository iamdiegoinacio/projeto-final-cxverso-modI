using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Application.Pagamentos;

/// <summary>
/// Forma de pagamento via Pix. Implementação simples do contrato IPagamento.
/// </summary>
public class PagamentoPix : IPagamento
{
    public string Nome => "Pix";

    public void Pagar(decimal valor)
    {
        // Em um sistema real, integraria com a API do banco/PSP.
        Console.WriteLine($"  [Pix] Transação iniciada — R$ {valor:F2}");
        Console.WriteLine($"  [Pix] QR Code gerado. Aguardando confirmação...");
        Console.WriteLine($"  [Pix] Pagamento confirmado com sucesso!");
    }
}
