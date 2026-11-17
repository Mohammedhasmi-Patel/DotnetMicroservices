using BuildingBlocks.CQRS;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductResult>;
public record GetProductResult(Product? Product);

public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var response = await session.Query<Product>().FirstOrDefaultAsync(p => p.Id == query.ProductId);
        if(response is null)
        {
            throw new NotFoundException("Product not found.");
        }
        return new GetProductResult(response);
    }
}
