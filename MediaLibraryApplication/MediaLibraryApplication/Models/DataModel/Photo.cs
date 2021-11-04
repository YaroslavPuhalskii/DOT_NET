using MediaLibraryApplication.Core;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Photo : File
    {
        public PhotoParameters Parameters { get; set; }
        public Photo(int id, string name, string format, PhotoParameters parameters)
            : base(id, name, format)
        {
            Parameters = parameters;
        }
    }
}
