using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using System;

namespace MediaLibraryApplication.Players
{
    public class VideoPlayer : IMediaPlayer
    {
        public void Play(MediaFile file)
        {
            Console.WriteLine($"Video : {file.Name} is playing!");
        }

        public void Play(IPlayList playList)
        {
            Console.WriteLine($"Video play list : {playList} is playing!");
        }
    }
}
