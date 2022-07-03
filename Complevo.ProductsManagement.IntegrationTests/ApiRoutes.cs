using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complevo.ProductsManagement.IntegrationTests
{
    public static class ApiRoutes
    {
        public const string RootApi = "api/";

        public static class Products
        {
            public const string GetAll = RootApi + "products";
            public const string GetById = RootApi + "products/{productId}";
            public const string Post = RootApi + "products";
            public const string PutById = RootApi + "products/{productId}";
        }
        

    }
}
