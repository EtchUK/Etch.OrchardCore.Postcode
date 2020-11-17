using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.Postcode.Models
{
    public class PostcodePart : ContentPart
    {
        public string Postcode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
