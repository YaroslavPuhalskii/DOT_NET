using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.Players;
using MediaLibraryApplication.Core.PlayList;
using System;

namespace MediaLibraryApplication.Players
{
    public class VideoPlayer : MediaPlayer
    {
        public override void Play(File file)
        {
            //doing something to play a video file
            Console.WriteLine($"Video : {file.Name} is playing!");
        }

        public override void Play(IPlayList playList)
        {
            //doing something to play a video playlist
            Console.WriteLine($"Video play list : {playList.Name} is playing!");
        }
    }
}
