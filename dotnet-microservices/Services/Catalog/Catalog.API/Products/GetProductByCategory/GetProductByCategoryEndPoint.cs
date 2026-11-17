namespace Catalog.API.Products.GetProductByCategory;


public record GetProductsByCategoryRequest(string Category);
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //throw new NotImplementedException();
        app.MapGet("/products/categories/{Category}", async (String Category, ISender sender) =>
        {
            var query = new GetProductsByCategoryQuery(Category);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(response);
        });
    }
}
