using Microsoft.Extensions.DependencyInjection;
using SistemaPedidos.Domain.Attributes;
using SistemaPedidos.Domain.Interfaces;
using System.Reflection;

namespace SistemaPedidos.Host.Extensions;

/// <summary>
/// Extensão do ServiceCollection que usa Reflection para descobrir e registrar
/// automaticamente todas as classes concretas que implementam IDesconto
/// e possuem o atributo [DescontoConfig].
///
/// Benefício (Bônus do enunciado + OCP):
/// Para adicionar um novo desconto, basta criar a classe com [DescontoConfig(...)].
/// Não é necessário tocar em Program.cs.
/// </summary>
public static class DescontoReflectionExtensions
{
    /// <summary>
    /// Escaneia o assembly informado, encontra todas as classes concretas que:
    /// 1. Implementam IDesconto
    /// 2. Possuem o atributo [DescontoConfig]
    /// E as registra no container com seus respectivos argumentos de construtor.
    /// </summary>
    public static IServiceCollection AddDescontosViaReflection(
        this IServiceCollection services,
        Assembly assembly)
    {
        var tipoIDesconto = typeof(IDesconto);
        var tipoAtributo  = typeof(DescontoConfigAttribute);

        var tiposDesconto = assembly
            .GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                tipoIDesconto.IsAssignableFrom(t) &&
                t.GetCustomAttribute(tipoAtributo) is not null);

        foreach (var tipo in tiposDesconto)
        {
            var atributo = (DescontoConfigAttribute)tipo.GetCustomAttribute(tipoAtributo)!;

            // Resolve os tipos dos parâmetros do construtor para conversão correta
            var construtorParams = tipo.GetConstructors().First().GetParameters();
            var argsConvertidos  = ConvertArgs(atributo.ConstructorArgs, construtorParams);

            // Cria a instância com Reflection usando os args configurados no atributo
            var instancia = (IDesconto)Activator.CreateInstance(tipo, argsConvertidos)!;

            services.AddSingleton(tipoIDesconto, instancia);
        }

        return services;
    }

    /// <summary>
    /// Converte os args do atributo (double por limitação do C#) para os tipos
    /// exatos do construtor (ex.: double → decimal, double → int).
    /// </summary>
    private static object[] ConvertArgs(object[] args, ParameterInfo[] parametros)
    {
        var resultado = new object[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            resultado[i] = Convert.ChangeType(args[i], parametros[i].ParameterType);
        }
        return resultado;
    }
}
