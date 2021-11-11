using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Photo : MediaFile
    {
        public PhotoParameters Parameters { get; set; }

        public Photo(int id, string name, string format, PhotoParameters parameters)
            : base(id, name, format)
        {
            Parameters = parameters;
        }

        public override void Play(IMediaPlayer player) => player.Play(this);
    }
}
