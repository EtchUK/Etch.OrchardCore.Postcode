using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.Postcode.Models
{
    public class PostcodeSearch : ContentPart
    {
        private const int DefaultPageSize = 10;

        public int PageSize { get; set; } = DefaultPageSize;

        public string Query { get; set; }

        #region UI Properties

        public string EmptyResultsContent { get; set; } = "Unable to find anything that matches your search.";

        public string PostcodeInputPlaceholder { get; set; } = "Enter search term...";

        public string SubmitButtonLabel { get; set; } = "Search";

        #endregion
    }
}
