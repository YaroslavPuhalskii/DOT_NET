using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Music : MediaFile
    {
        public MusicParameters Parameters { get; set; }

        public Music(int id, string name, string format, MusicParameters parameters)
            : base(id, name, format)
        {
            Parameters = parameters;
        }

        public override void Play(IMediaPlayer player) => player.Play(this);
    }
}
