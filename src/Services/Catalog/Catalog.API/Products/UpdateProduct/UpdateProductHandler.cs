using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);


    /// <summary>
    ///  Perform Validation on the Command
    /// </summary>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand> 
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 100).WithMessage("Name should be between 3 and 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price should be greater than 0");
        }

    }

    public class UpdateProductCommandHandler(IDocumentSession session
        , ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating product {@command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null)
            {
               // throw new InvalidOperationException($"Product with id {command.Id} not found");
               throw new ProductNotFoundException(command.Id);
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

             session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);




            // throw new NotImplementedException();
        }
    }

}
