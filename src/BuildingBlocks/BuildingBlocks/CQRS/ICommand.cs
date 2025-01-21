using MediatR;

namespace BuildingBlocks
{
    /// <summary>
    /// Empty ICommand Interface Does Not return any response
    /// </summary>
    public interface ICommand : IRequest<Unit>
    {
    }
    /// <summary>
    /// ICommand Interface that returns a response
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
