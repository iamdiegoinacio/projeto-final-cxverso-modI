namespace SistemaPedidos.Domain.Entities;

/// <summary>
/// Entidade de domínio representando um produto disponível para venda.
/// Valida suas próprias invariantes no construtor (Fail-Fast).
/// </summary>
public class Produto : IEntity
{
    public int Id { get; }
    public string Nome { get; }
    public decimal Preco { get; }

    public Produto(int id, string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));

        if (preco <= 0)
            throw new ArgumentException("O preço do produto deve ser maior que zero.", nameof(preco));

        Id = id;
        Nome = nome;
        Preco = preco;
    }

    public override string ToString() => $"{Nome} (R$ {Preco:F2})";
}
