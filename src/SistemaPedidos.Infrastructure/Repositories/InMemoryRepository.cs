using SistemaPedidos.Domain.Entities;
using SistemaPedidos.Domain.Interfaces;

namespace SistemaPedidos.Infrastructure.Repositories;

/// <summary>
/// Implementação de repositório genérico em memória.
/// Reutilizável para qualquer entidade que implemente IEntity.
/// Em produção, seria substituído por uma implementação com banco de dados.
/// </summary>
public class InMemoryRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _store = new();

    public void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _store.Add(entity);
    }

    public IEnumerable<T> GetAll() => _store.AsReadOnly();
}
