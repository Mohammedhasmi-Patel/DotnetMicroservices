using BuildingBlocks.CQRS;
using FluentValidation;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IQuery<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }
}

public class DeleteProductHandler(IDocumentSession session) : IQueryHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        var executionId = Guid.NewGuid();

        Console.WriteLine($"[{executionId}] Handler started");

        var product = await session.LoadAsync<Product>(
            command.Id, cancellationToken);

        Console.WriteLine(product is null
            ? $"[{executionId}] PRODUCT NOT FOUND"
            : $"[{executionId}] PRODUCT FOUND: {product.Id}");

        Console.WriteLine(product is null
        ? "PRODUCT NOT FOUND"
        : $"PRODUCT FOUND: {product.Id}");

        if(product is null)
        {
            throw new NotFoundException("Product not found.");
        }
        session.Delete(product);
        await session.SaveChangesAsync();
        return new DeleteProductResult(true);
    }
}
