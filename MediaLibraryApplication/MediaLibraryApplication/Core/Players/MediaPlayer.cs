using MediaLibraryApplication.Core.PlayList;

namespace MediaLibraryApplication.Core.Players
{
    public abstract class MediaPlayer
    {
        public abstract void Play(File file);

        public abstract void Play(IPlayList playList);
    }
}
