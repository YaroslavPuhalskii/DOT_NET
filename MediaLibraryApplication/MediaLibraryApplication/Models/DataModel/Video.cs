using MediaLibraryApplication.Core;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Video : File
    {
        public VideoParameters Parameters { get; set; }

        public Video(int id, string name, VideoParameters parameters) : base(id, name)
        {
            Parameters = parameters;
        }
    }
}
