using Microsoft.AspNetCore.Mvc.Testing;

namespace Complevo.ProductsManagement.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
    }
}
