using Microsoft.Extensions.Configuration;
using TestSupport.Helpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace ReaAccountingSys.IntegrationTests
{
    public class AppSettingsTests
    {
        [Fact]
        public void ShouldGetConnStringFromAppSettings()
        {
            //SETUP

            //ATTEMPT
            var config = AppSettings.GetConfiguration();

            //VERIFY
            config.GetConnectionString("DefaultConnection")
                .ShouldEqual("Server=tcp:mssql-server,1433;Database=Pipefitters_Test;User Id=sa;Password=Info99Gum;TrustServerCertificate=True");
        }
    }
}