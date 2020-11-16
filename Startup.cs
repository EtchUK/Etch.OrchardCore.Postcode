using Etch.OrchardCore.PostcodeSearch;
using Etch.OrchardCore.PostcodeSearch.Drivers;
using Etch.OrchardCore.PostcodeSearch.Models;
using Etch.OrchardCore.PostcodeSearch.ViewModels;
using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using UKIE.OrchardCore.PostcodeSearch.Indexes;
using YesSql.Indexes;

namespace Etch.OrchardCore.PeopleContentFeed
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<PostcodeViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ContentPart, PostcodeSearch.Models.PostcodeSearch>();
            services.AddSingleton<ContentPart, PostcodePart>();

            services.AddScoped<IContentPartDisplayDriver, PostcodePartDisplay>();
            services.AddScoped<IContentPartDisplayDriver, PostcodeSearchPartDisplay>();

            services.AddScoped<IDataMigration, Migrations>();
            services.AddSingleton<IIndexProvider, PostcodeIndexProvider>();

        }
    }
}