using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Video : MediaFile
    {
        public VideoParameters Parameters { get; set; }

        public Video(int id, string name, string format, VideoParameters parameters)
            : base(id, name, format)
        {
            Parameters = parameters;
        }

        public override void Play(IMediaPlayer player) => player.Play(this);
    }
}
