using MediatR;

namespace BuildingBlocks.CQRS
{

    /// <summary>
    /// ICommandHandler Interface that does not return any response
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> 
        : ICommandHandler<TCommand , Unit>
        where TCommand : ICommand<Unit>
    {
    }



    /// <summary>
    /// ICommandHandler Interface that returns a response
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>

    public interface ICommandHandler<in TCommand , TResponse> 
        : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
