using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.Players;
using MediaLibraryApplication.Core.PlayList;
using System;

namespace MediaLibraryApplication.Players
{
    public class MusicPlayer : MediaPlayer
    {
        public override void Play(File file)
        {
            //doing something to play a music file
            Console.WriteLine($"Music : {file.Name} is playing!");
        }

        public override void Play(IPlayList playList)
        {
            //doing something to play a music playlist
            Console.WriteLine($"Music play list : {playList.Name} is playing!");
        }
    }
}
