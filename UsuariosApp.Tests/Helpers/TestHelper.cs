using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Tests.Helpers
{
    public static class TestHelper
    {
        public static StringContent Serialize(object request)
            => new StringContent
            (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        public static HttpClient CreateClient()
            => new WebApplicationFactory<Program>().CreateClient();


    }
}
