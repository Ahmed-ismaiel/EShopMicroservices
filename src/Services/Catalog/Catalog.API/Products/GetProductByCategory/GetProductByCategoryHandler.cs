using System.Collections.Generic;

namespace Catalog.API.Products.GetProductByCategory
{


    public record GetProductByCategoryQuery(string CategoryId) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Product);

    public class GetProductByCategoryHandler (IDocumentSession session 
        , ILogger<GetProductByCategoryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {

            logger.LogInformation("Querying for products in category {CategoryId}", query.CategoryId);

            var products = await  session.Query<Product>()
                .Where(p => p.Category.Contains(query.CategoryId))
                .ToListAsync(cancellationToken);

            //if (products.Count == 0)              
            //{
            //   logger.LogWarning("No products found for category {CategoryId}", query.CategoryId);
            //}

            return new GetProductByCategoryResult(products);
            
            //throw new NotImplementedException();
        }
    }   
   
}
