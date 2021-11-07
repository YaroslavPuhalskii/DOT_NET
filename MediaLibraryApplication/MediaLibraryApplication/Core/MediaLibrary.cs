using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using System;
using System.Collections.Generic;

namespace MediaLibraryApplication.Models
{
    public class MediaLibrary : IMediaLibrary
    {
        public IDictionary<Type, IMediaPlayer> playersMap;

        public  PlayList DefaultPlaylist { get; }

        public ICollection<IPlayList> PlayLists { get; set; }


        public MediaLibrary(IDictionary<Type, IMediaPlayer> players, PlayList playList)
        {
            playersMap = players;
            DefaultPlaylist = playList;
            PlayLists = new List<IPlayList>();
        }

        public void Play(MediaFile file)
        {
            if (!playersMap.TryGetValue(file.GetType(), out var player))
            {
                throw new NotImplementedException();
            }

            player.Play(file);
        }

        public void PlayPlaylist()
        {
            if (PlayLists == null)
                throw new ArgumentNullException();

            foreach (var playlist in PlayLists)
            {
                PlayFiles(playlist.MediaFiles);
            }
        }

        private void PlayFiles(IEnumerable<MediaFile> files)
        {
            foreach (var file in files)
            {
                Play(file);
            }
        }
    }
}
