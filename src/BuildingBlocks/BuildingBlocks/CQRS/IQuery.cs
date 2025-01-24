using MediatR;

namespace BuildingBlocks
{
    /// <summary>
    /// IQuery Interface that returns a response
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery<TResponse> 
        : IRequest<TResponse>  
        where TResponse : notnull
    {
    }
   
}
