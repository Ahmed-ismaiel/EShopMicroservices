using BuildingBlocks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
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
   //  : IRequest<CreateProductResult>;
       : ICommand<CreateProductResult>;
    
    /// <summary>
    /// The Type of Result (Response) that will be returned after creating a product
    public record CreateProductResult(Guid Id);

    //public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    //{
    //    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    //    {
    //        // business logic to create a product
    //        throw new NotImplementedException();
    //    }
    //}

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
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

            // 3 - Return CreateProductResult with the Id of the newly created product

            return new CreateProductResult(Guid.NewGuid());


        }
    }


}
