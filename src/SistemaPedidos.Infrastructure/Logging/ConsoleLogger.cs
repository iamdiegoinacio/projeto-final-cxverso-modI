using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Infrastructure.Logging;

/// <summary>
/// Implementação de ILogger que escreve mensagens formatadas no console.
/// Detalhes de infraestrutura (cor do console, timestamp) ficam isolados aqui.
/// </summary>
public class ConsoleLogger : ILogger
{
    public void Info(string message)
    {
        var cor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{message}");
        Console.ForegroundColor = cor;
    }

    public void Error(string message)
    {
        var cor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERRO] {message}");
        Console.ForegroundColor = cor;
    }
}
