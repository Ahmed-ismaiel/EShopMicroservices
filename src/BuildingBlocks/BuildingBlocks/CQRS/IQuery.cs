using MediatR;

namespace BuildingBlocks
{
    public interface IQuery<TResponse> : IRequest<TResponse>  where TResponse : notnull
    {
    }
   
}
