using System.Collections.Concurrent;
using System.Reflection;
using DotNetDemo.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetDemo.Infrastructure.Dispatching;

public sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> HandleMethodCache = new();

    public Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
        var handler = serviceProvider.GetRequiredService(handlerType);

        var method = HandleMethodCache.GetOrAdd(
            queryType,
            _ => handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))!);

        return (Task<TResult>)method.Invoke(handler, [query, cancellationToken])!;
    }
}
