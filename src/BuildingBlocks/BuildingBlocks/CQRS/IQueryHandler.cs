using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// IQueryHandler Interface that returns a response
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQueryHandler<in TQuery , TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
