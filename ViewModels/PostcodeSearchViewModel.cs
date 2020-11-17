using GeoCoordinatePortable;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.Postcode.ViewModels
{
    public class PostcodeSearchViewModel
    {
        public ContentItem ContentItem { get; set; }
        public string Postcode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public GeoCoordinate GeoCoordinate
        {
            get
            {
                return new GeoCoordinate(Latitude, Longitude);
            }
        }
    }
}
