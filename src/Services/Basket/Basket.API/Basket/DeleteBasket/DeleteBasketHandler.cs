﻿namespace Basket.API.Basket.DeleteBasket
{

    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsDeleted);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("UserName Can not be null or empty");
        }
    }


    public class DeleteBasketCommandHandler(IBasketRepository basketRepository)  
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO : Delete from Database and cash
            // Session.Delete(command.UserName);

            await basketRepository.DeleteBasket(command.UserName,cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
    
}
