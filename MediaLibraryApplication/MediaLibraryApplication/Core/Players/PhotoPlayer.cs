using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using System;

namespace MediaLibraryApplication.Players
{
    public class PhotoPlayer : IMediaPlayer
    {
        public void Play(MediaFile file)
        {
            Console.WriteLine($"Photo : {file.Name} is showing!");
        }

        public void Play(IPlayList playList)
        {
            Console.WriteLine($"Photo play list : {playList} is showing!");
        }
    }
}
