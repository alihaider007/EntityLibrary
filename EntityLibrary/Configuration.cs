using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLibrary
{
    public static class Configuration
    {
        // https://zimmergren.net/using-appsettings-json-instead-of-web-config-in-net-core-projects/
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("configuration.json",
                optional: true,
                reloadOnChange: true);

            return builder.Build();
        }
    }
}
