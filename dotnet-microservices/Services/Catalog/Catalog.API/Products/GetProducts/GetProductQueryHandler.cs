using BuildingBlocks.CQRS;
using Marten.Linq.QueryHandlers;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);
public class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        int pageNumber = query.PageNumber ?? 1;
        int pageSize = query.PageSize ?? 10;

        var products = await session.Query<Product>().ToPagedListAsync(pageNumber,pageSize,cancellationToken);
        return new GetProductResult(products);
    }
}
