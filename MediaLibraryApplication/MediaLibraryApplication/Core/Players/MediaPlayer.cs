using MediaLibraryApplication.Core.PlayList;

namespace MediaLibraryApplication.Core.Players
{
    public abstract class MediaPlayer
    {
        public virtual void Play(File file)
        {
            //
        }

        public virtual void Play(IPlayList playList)
        {
            //
        }
    }
}
