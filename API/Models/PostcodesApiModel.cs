using System.Collections.Generic;

namespace Etch.OrchardCore.Postcode.API.Models
{
    public class PostcodesApiModel
    {
        public List<ResultsModel> Result { get; set; }
        public ResultsModel FirstResult
        {
            get
            {
                if (Result != null && Result.Count > 0)
                {
                    return Result[0];
                }

                return new ResultsModel();
            }
        }
    }

    public class ResultsModel
    {
        public string Postcode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
