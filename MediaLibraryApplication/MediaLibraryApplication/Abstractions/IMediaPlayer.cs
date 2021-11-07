using MediaLibraryApplication.Core.Media;

namespace MediaLibraryApplication.Abstractions
{
    public interface IMediaPlayer
    {
        void Play(MediaFile file);

        void Play(IPlayList playList);
    }
}
