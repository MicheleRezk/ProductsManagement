using System.ComponentModel.DataAnnotations;

namespace Complevo.ProductsManagement.Dtos
{
    public record ProductDto(Guid Id, string Name, string Description);
    public record CreateProductDto([Required] string Name, string Description);
    public record UpdateProductDto([Required] string Name, string Description);
}
