namespace SistemaPedidos.Domain.Attributes;

/// <summary>
/// Atributo que marca uma implementação de IDesconto para ser
/// auto-registrada via Reflection no container de DI.
/// Permite configurar os parâmetros do construtor de forma declarativa.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class DescontoConfigAttribute : Attribute
{
    /// <summary>
    /// Argumentos passados ao construtor da classe decorada, em ordem de declaração.
    /// </summary>
    public object[] ConstructorArgs { get; }

    public DescontoConfigAttribute(params object[] constructorArgs)
    {
        ConstructorArgs = constructorArgs;
    }
}
