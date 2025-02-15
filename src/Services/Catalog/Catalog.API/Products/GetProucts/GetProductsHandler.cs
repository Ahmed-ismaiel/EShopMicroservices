
namespace Catalog.API.Products.GetProucts
{
    // added here to use pagination
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
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
            
           // var products = await session.Query<Product>().ToListAsync(cancellationToken);
           
            // Here instead of usiing tolist we can use pagination Topagelist
            
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1 , query.PageSize ?? 10 ,cancellationToken);

            return new GetProductsResult(products);

        }
    }
}
