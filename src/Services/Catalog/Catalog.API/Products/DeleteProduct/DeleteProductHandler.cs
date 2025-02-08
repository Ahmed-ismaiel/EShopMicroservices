namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    public class DeleteProductHandler(IDocumentSession session
        ,ILogger<DeleteProductCommand> logger )  
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting product {@command}", command);
            var product = await session.LoadAsync<Product>(command.Id);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with id {command.Id} not found");
            }
            session.Delete(product);
            
            return new DeleteProductResult(true);

        }
    }
}
