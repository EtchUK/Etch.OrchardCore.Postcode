using System.Threading.Tasks;
using Etch.OrchardCore.PostcodeSearch.API;
using Etch.OrchardCore.PostcodeSearch.Models;
using Etch.OrchardCore.PostcodeSearch.ViewModels;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;


namespace Etch.OrchardCore.PostcodeSearch.Drivers
{

    public class PostcodePartDisplay : ContentPartDisplayDriver<PostcodePart>
    {
        #region Dependencies

        private readonly IStringLocalizer<PostcodePartDisplay> T;

        #endregion

        #region Constructor

        public PostcodePartDisplay(IStringLocalizer<PostcodePartDisplay> localizer)
        {
            T = localizer;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Display(PostcodePart part, BuildPartDisplayContext context)
        {
            return null;
        }

        public override IDisplayResult Edit(PostcodePart part, BuildPartEditorContext context)
        {
            return Initialize<PostcodeViewModel>("PostcodePart_Edit", model =>
            {
                model.Postcode = part.Postcode;
                model.Longitude = part.Longitude;
                model.Latitude = part.Latitude;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(PostcodePart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var model = new PostcodeViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix))
            {
                var response = await PostcodesApi.GetGameData(model.Postcode);

                part.Postcode = model.Postcode;
                part.Longitude = response != null && !string.IsNullOrEmpty(response.FirstResult.Postcode) ? response.FirstResult.Longitude : model.Longitude;
                part.Latitude = response != null && !string.IsNullOrEmpty(response.FirstResult.Postcode) ? response.FirstResult.Latitude : model.Latitude;
            }

            if (!string.IsNullOrEmpty(model.Postcode) && string.IsNullOrEmpty(part.Longitude) && string.IsNullOrEmpty(part.Latitude))
            {
                updater.ModelState.AddModelError(Prefix, nameof(part.Postcode), T["Please enter a valid postcode or longitude, latitude."]);
            }

            return Edit(part, context);
        }

        #endregion
    }
}
