using System.ComponentModel.DataAnnotations;

namespace Complevo.ProductsManagement.Dtos
{
    public record ProductDto(Guid Id, string Name, string description);
    public record CreateProductDto([Required] string Name, string description);
    public record UpdateProductDto([Required] Guid Id, [Required] string Name, string description);
    public record DeleteProductDto([Required] Guid Id);
}
