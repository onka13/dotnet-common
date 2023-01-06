using Autofac;
using DotNetCommon.Data.Domain.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCommon.Application.WebAPIBase
{
    public class ModuleConfig : IModuleConfig
    {
        public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }
    }
}
