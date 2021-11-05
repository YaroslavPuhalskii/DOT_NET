using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.Players;
using MediaLibraryApplication.Core.PlayList;
using System;

namespace MediaLibraryApplication.Players
{
    public class VideoPlayer : MediaPlayer
    {
        /// <summary>
        /// Method for playing a video file
        /// </summary>
        /// <param name="file"></param>
        public override void Play(File file)
        {
            //doing something to play a video file
            Console.WriteLine($"Video : {file.Name} is playing!");
        }
        /// <summary>
        /// Method for playing a video playlist
        /// </summary>
        /// <param name="playList"></param>
        public override void Play(IPlayList playList)
        {
            //doing something to play a video playlist
            Console.WriteLine($"Video play list : {playList.Name} is playing!");
        }
    }
}
