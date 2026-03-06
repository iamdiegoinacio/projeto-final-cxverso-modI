using SistemaPedidos.Domain.Entities;

namespace SistemaPedidos.Domain.Interfaces;

/// <summary>
/// Repositório genérico. Constraint garante que apenas entidades de domínio
/// (que implementam IEntity) possam ser gerenciadas.
/// </summary>
public interface IRepository<T> where T : IEntity
{
    void Add(T entity);
    IEnumerable<T> GetAll();
}
