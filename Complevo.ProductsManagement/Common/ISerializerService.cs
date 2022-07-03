namespace Complevo.ProductsManagement.Common
{
    public interface ISerializerService
    {
        T Deserialize<T>(string json);
    }
}
