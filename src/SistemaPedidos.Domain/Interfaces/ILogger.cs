namespace SistemaPedidos.Domain.Interfaces;

/// <summary>
/// Contrato de logging. Colocado no Domain para que nenhuma camada de regra
/// precise depender da Infrastructure (DIP).
/// </summary>
public interface ILogger
{
    void Info(string message);
    void Error(string message);
}
