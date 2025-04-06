
namespace Basket.API.Basket.StoreBasket;



public record StoreBaskeCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

// instead of returning bool, we can return a username 
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBaskeCommand> 
{

    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Cart Can not be null");
        RuleFor(x => x.ShoppingCart.UserName).NotNull().NotEmpty().WithMessage("UserName Can not be null or empty");

    }




}

public class StoreBasketCommandHandler(IBasketRepository basketRepository) 
    : ICommandHandler<StoreBaskeCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBaskeCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart shoppingCart = command.ShoppingCart;
        //TODO : Save to Database (use marten if exist = update, if not creat )
        await basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);
        //TODO : Update Cache                                        

        return new StoreBasketResult(command.ShoppingCart.UserName);
    }
}

