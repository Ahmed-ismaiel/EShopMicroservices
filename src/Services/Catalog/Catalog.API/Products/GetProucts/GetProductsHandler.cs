
using JasperFx.CodeGeneration.Frames;

namespace Catalog.API.Products.GetProucts
{

    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);



    public class GetProductsQueryHandler(IDocumentSession session 
        ) 
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            //log.LogInformation("Getting all products");
            //logger.LogInformation("Getting all products {@query}" , query);

            // business logic to get all products

            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);

        }
    }
}
