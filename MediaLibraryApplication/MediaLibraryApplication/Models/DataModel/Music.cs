using MediaLibraryApplication.Core;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Music : File
    {
        public MusicParameters Parameters;
        public Music(int id, string name, string format, MusicParameters parameters)
            : base(id, name, format)
        {
            Parameters = parameters;
        }
    }
}
