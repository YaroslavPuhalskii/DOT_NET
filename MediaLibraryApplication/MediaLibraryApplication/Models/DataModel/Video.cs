using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.PlayList;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Video : File
    {
        public VideoParameters Parameters { get; set; }

        public Video(int id, string name, string format, VideoParameters parameters) 
            : base(id, name, format)
        {
            Parameters = parameters;
        }
    }
}
