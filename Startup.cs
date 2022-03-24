using Etch.OrchardCore.Postcode;
using Etch.OrchardCore.Postcode.Drivers;
using Etch.OrchardCore.Postcode.Models;
using Etch.OrchardCore.Postcode.ViewModels;
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
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<PostcodeViewModel>();
            });

            services.AddContentPart<PostcodeSearch>()
                .UseDisplayDriver<PostcodeSearchPartDisplay>();

            services.AddContentPart<PostcodePart>()
                .UseDisplayDriver<PostcodePartDisplay>();

            services.AddScoped<IDataMigration, Migrations>();
            services.AddSingleton<IIndexProvider, PostcodeIndexProvider>();

        }
    }
}