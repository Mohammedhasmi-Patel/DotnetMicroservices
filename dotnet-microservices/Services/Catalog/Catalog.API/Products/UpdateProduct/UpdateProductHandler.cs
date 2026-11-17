using BuildingBlocks.CQRS;
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name,List<string> Category,string Description,string ImageFile,decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("id is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Category)
            .NotNull()
            .NotEmpty()
            .Must(categories => categories.All(c => !string.IsNullOrWhiteSpace(c)))
            .WithMessage("Categories cannot contain empty values.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("Image is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}
public class UpdateProductCommandHandler(IDocumentSession session, IValidator<UpdateProductCommand> validator) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new Exception("Product not found.");
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync();
        return new UpdateProductResult(true);
    }
}
