using System;
using System.IO;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace consumer_ui
{
    // Used to configure the authentication required to 
    // use the Web API. 
    public class AuthConfig
    {
        public string Instance {get; set;} =
            "https://login.microsoftonline.com/{0}";
        public string TenantId {get; set;}
        public string ClientId {get; set;}
        public string Authority
        {
        get
        {
            return String.Format(CultureInfo.InvariantCulture, 
                                Instance, TenantId);
        }
        }
        public string ClientSecret {get; set;}
        public string BaseAddress {get; set;}
        public string ResourceId {get; set;}

        // Reads contents from appsettings.json, and the class
        // properties will then bind to the json attributes of 
        // the same name. 
        public static AuthConfig ReadFromJsonFile(string path)
        {
            IConfiguration Configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path);

            Configuration = builder.Build();

            return Configuration.Get<AuthConfig>();
        }
    }
}