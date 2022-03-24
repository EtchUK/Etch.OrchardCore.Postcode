using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using UKIE.OrchardCore.PostcodeSearch.Indexes;

namespace Etch.OrchardCore.Postcode
{
    public class Migrations : DataMigration
    {
        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;

        #endregion Dependencies

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
        }

        #endregion Constructor

        #region Migrations

        public async Task<int> CreateAsync()
        {
            _contentDefinitionManager.AlterPartDefinition("PostcodePart", part => part
                .Attachable()
                .WithDisplayName("Postcode")
                .WithDescription("Adds postcode, longitude, latitude.")
            );

            SchemaBuilder.CreateMapIndexTable(typeof(PostcodeIndex), table => table
                .Column<string>("Postcode", c => c.WithDefault(null))
                .Column<string>("Longitude", c => c.WithDefault(null))
                .Column<string>("Latitude", c => c.WithDefault(null)),
                null
            );

            _contentDefinitionManager.AlterPartDefinition("PostcodeSearch", part => part
                .WithDescription("Adds postcode search to page.")
                .WithDisplayName("Postcode Search"));

            await _recipeMigrator.ExecuteAsync("create.recipe.json", this);

            return 1;
        }

        #endregion Migrations
    }
}