using Etch.OrchardCore.PostcodeSearch.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace UKIE.OrchardCore.PostcodeSearch.Indexes
{
    public class PostcodeIndex : MapIndex
    {
        public string Postcode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public class PostcodeIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<PostcodeIndex>()
                .Map(contentItem =>
                {
                    if (contentItem.As<PostcodePart>() == null)
                    {
                        return null;
                    }

                    return new PostcodeIndex
                    {
                        Postcode = contentItem?.As<PostcodePart>()?.Postcode ?? string.Empty,
                        Longitude = contentItem?.As<PostcodePart>()?.Longitude ?? string.Empty,
                        Latitude = contentItem?.As<PostcodePart>()?.Latitude ?? string.Empty
                    };
                });
        }

        private string GetValueWithDefault(ContentItem contentItem, string fieldName, string defaultValue)
        {
            return contentItem?.As<PostcodePart>()?.Latitude ?? defaultValue;
        }
    }
}