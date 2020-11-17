using System.Linq;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.Postcode.ViewModels
{
    public class PostcodeSearchListViewModel
    {
        public ContentItem[] Results { get; set; }

        public bool HasResults
        {
            get { return Results != null && Results.Any(); }
        }

        public string EmptyResultsContent { get; set; }

        public string PostcodeInputPlaceholder { get; set; }

        public string Postcode { get; set; }

        public string SubmitButtonLabel { get; set; }

        public bool IsSearching
        {
            get { return !string.IsNullOrWhiteSpace(Postcode); }
        }
    }
}
