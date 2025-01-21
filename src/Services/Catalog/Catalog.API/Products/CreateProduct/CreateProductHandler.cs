using MediatR;

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
     : IRequest<CreateProductResult>;
    /// <summary>
    /// The Type of Result (Response) that will be returned after creating a product
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // business logic to create a product
            throw new NotImplementedException();
        }
    }
}
