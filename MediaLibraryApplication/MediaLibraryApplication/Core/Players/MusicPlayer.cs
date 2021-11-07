using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using System;

namespace MediaLibraryApplication.Players
{
    public class MusicPlayer : IMediaPlayer
    {
        public void Play(MediaFile file)
        {
            Console.WriteLine($"Music : {file.Name} is playing!");
        }

        public void Play(IPlayList playList)
        {
            Console.WriteLine($"Photo play list : {playList} is showing!");
        }
    }
}
