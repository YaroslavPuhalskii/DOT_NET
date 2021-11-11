using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Models.DataModel;

namespace MediaLibraryApplication.Core.Players
{
    public class MediaPlayer : IMediaPlayer
    {
        public void Play(IFile media) { }

        /*
        public void Play(Music media) { }

        public void Play(Video media) { }

        public void Play(Photo media) { }

        public void Play(IPlayList media) { }
        */
    }
}
