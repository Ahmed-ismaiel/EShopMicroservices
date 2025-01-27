

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpiont : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            /// <summary>
            /// post /products - Create a new product 
            /// takes request and Isender (which send req through mediator pipeline)


            app.MapPost("/products", 
                async (CreateProductRequest request, ISender sender) =>
            {
                // Command to create a new product
                // b3ml map mn l data ly fel request le l object 
                var command = request.Adapt<CreateProductCommand>();

                // Send the command (Request ) through the mediator pipeline to be handled by the CreateProductCommandHandler
                var result = await sender.Send(command);

                // Adapt (Mapped) the result to the response
                var response = result.Adapt<CreateProductResponse>();

                // Return the response of newly created product
                return Results.Created($"/products/{response.Id}", response);

            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(201)
                .ProducesProblem(400)
                .WithSummary("Create a new product")
                .WithDescription("Create a new product with the given data");

        }
    }
}
