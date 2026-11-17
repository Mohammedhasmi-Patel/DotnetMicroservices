using BuildingBlocks.CQRS;
using FluentValidation;
using Marten.Linq.QueryHandlers;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryValidator : AbstractValidator<GetProductsByCategoryQuery>
{
    public GetProductsByCategoryQueryValidator()
    {
        RuleFor(p => p.Category)
               .NotEmpty()
               .WithMessage("The Category is required.");
    }
}
public class GetProductByCategoryHandler(IDocumentSession session, IValidator<GetProductsByCategoryQuery> validator) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        
        var results = await session.Query<Product>()
                        .Where(c => c.Category.Contains(query.Category))
                        .ToListAsync();

        return new GetProductsByCategoryResult(results);
    }
}
