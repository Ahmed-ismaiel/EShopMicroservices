
using Discount.Grpc;

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

public class StoreBasketCommandHandler
    (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBaskeCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBaskeCommand command, CancellationToken cancellationToken)
    {
        //  ShoppingCart shoppingCart = command.ShoppingCart;

        //TODO : Communicate with Discount Service to get the discounts for the items in the cart
        // This can be done using a gRPC call or an HTTP call to the Discount Service

        await DeductDiscount(command.ShoppingCart, cancellationToken);



        //TODO : Save to Database (use marten if exist = update, if not creat )
        await basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);
        //TODO : Update Cache                                        

        return new StoreBasketResult(command.ShoppingCart.UserName);
    }

    /// <summary>
    ///  hena bgeb l discount mn l discount service 3shan atb2 3la l items fl cart
    ///  b3ml loop 3la kol item fl cart w bgeb l discount mn l discount service 
    /// </summary>
    /// <param name="shoppingCart"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeductDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        foreach (var item in shoppingCart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }



}

