namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id);
public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"DELETE REQUEST RECEIVED: {id}");
            var command = new DeleteProductCommand(id);
            var response = await sender.Send(command);
            return Results.Ok(response);
        });
    }
}
