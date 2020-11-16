using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.PostcodeSearch.API;
using Etch.OrchardCore.PostcodeSearch.Models;
using Etch.OrchardCore.PostcodeSearch.ViewModels;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.Views;
using UKIE.OrchardCore.PostcodeSearch.Indexes;
using YesSql;

namespace Etch.OrchardCore.PostcodeSearch.Drivers
{
    public class PostcodeSearchPartDisplay : ContentPartDisplayDriver<Models.PostcodeSearch>
    {
        #region Constants

        public const string QueryStringPostcode = "Postcode";

        #endregion

        #region Dependencies

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly YesSql.ISession _session;
        private readonly IStringLocalizer<PostcodeSearchPartDisplay> T;

        #endregion

        #region Constructor

        public PostcodeSearchPartDisplay(IHttpContextAccessor httpContextAccessor, IStringLocalizer<PostcodeSearchPartDisplay> localizer, YesSql.ISession session)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = session;
            T = localizer;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> DisplayAsync(Models.PostcodeSearch part, BuildPartDisplayContext context)
        {
            var contents = new List<PostcodeSearchViewModel>();
            var results = Enumerable.Empty<ContentItem>();
            ContentItem[] list = null;

            var postcode = GetQuerystringValue(QueryStringPostcode);
            var response = await PostcodesApi.GetGameData(postcode);

            if (response != null && !string.IsNullOrEmpty(postcode))
            {
                var coord = new GeoCoordinate(Convert.ToDouble(response.FirstResult.Latitude), Convert.ToDouble(response.FirstResult.Longitude));

                results = await _session.Query<ContentItem>()
                     .With<ContentItemIndex>()
                        .Where(x => x.Published)
                     .With<PostcodeIndex>()
                     .ListAsync();

                foreach (var result in results)
                {
                    contents.Add(new PostcodeSearchViewModel
                    {
                        ContentItem = result,
                        Latitude = Convert.ToDouble(result?.ContentItem?.As<PostcodePart>()?.Latitude ?? null),
                        Longitude = Convert.ToDouble(result?.ContentItem?.As<PostcodePart>()?.Longitude ?? null),
                        Postcode = result?.ContentItem?.As<PostcodePart>()?.Postcode
                    });
                }

                list = contents.OrderBy(x => x.GeoCoordinate.GetDistanceTo(coord)).ToList().Select(x => x.ContentItem).ToArray();
            }

            return Initialize<PostcodeSearchListViewModel>("PostcodeSearch_List", model =>
            {
                model.EmptyResultsContent = part.EmptyResultsContent;
                model.Postcode = postcode;
                model.PostcodeInputPlaceholder = part.PostcodeInputPlaceholder;
                model.Results = list;
                model.SubmitButtonLabel = part.SubmitButtonLabel;
            })
            .Location("Detail", "Content:5");
        }

        #endregion

        #region Helpers

        private string GetQuerystringValue(string key)
        {
            var query = _httpContextAccessor.HttpContext.Request.Query;
            if (!query.Keys.Contains(key))
            {
                return null;
            }
            return query[key];
        }

        #endregion
    }
}
