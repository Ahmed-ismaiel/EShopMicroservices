
namespace Catalog.API.Products.CreateProduct
{
    /// <summary>
    /// Command to create a new product
    /// Data Needed To create a Product
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Category"></param>
    /// <param name="Description"></param>
    /// <param name="ImageFile"></param>
    /// <param name="Price"></param>
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
   //  : IRequest<CreateProductResult>;
       : ICommand<CreateProductResult>;
    
    /// <summary>
    /// The Type of Result (Response) that will be returned after creating a product
    public record CreateProductResult(Guid Id);

    /// <summary>
    /// Validator
    /// </summary>
    /// <param name="session"></param>

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            //RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater thank 0");
        }
    }


    //public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    //{
    //    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    //    {
    //        // business logic to create a product
    //        throw new NotImplementedException();
    //    }
    //}

    public class CreateProductCommandHandler(IDocumentSession session
        , ILogger<CreateProductCommandHandler> logger) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            logger.LogInformation("createproductHandler {@Command}", command);

            // business logic to create a product


            // 1 - Create a new Product Entiy with the data from the command

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price

            };

            // 2 - Save the product to the database

            // 2.1 - Store the product in the session
            session.Store(product);

            // 2.2 - Save the changes to the database

            await session.SaveChangesAsync(cancellationToken);


            // 3 - Return CreateProductResult with the Id of the newly created product

            return new CreateProductResult(product.Id);


        }
    }


}
