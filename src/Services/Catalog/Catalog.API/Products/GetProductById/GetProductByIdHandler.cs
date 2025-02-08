
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdEndpoint> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {

            logger.LogInformation("Getting product by id {@query}", query);

            // business logic to get product by id

            var productById = await session.LoadAsync<Product>(query.Id ,cancellationToken);

            if (productById is null)
            {

                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(productById);

            // throw new NotImplementedException();
        }
    }
}
