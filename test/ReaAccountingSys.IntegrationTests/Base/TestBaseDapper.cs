using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using TestSupport.Helpers;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;

namespace ReaAccountingSys.IntegrationTests.Base
{
    [Trait("Integration", "DapperQuerySvc")]
    public abstract class TestBaseDapper
    {
        protected readonly DapperContext _dapperCtx;
        protected readonly string serviceAddress = "https://localhost:7035/";

        public TestBaseDapper()
        {
            var config = AppSettings.GetConfiguration();
            _dapperCtx = new DapperContext(config.GetConnectionString("DefaultConnection"));

            using (var connection = _dapperCtx.CreateConnection())
            {
                connection.Execute("dbo.usp_resetTestDb", commandType: CommandType.StoredProcedure);
            }
        }
    }
}