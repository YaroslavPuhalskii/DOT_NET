using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibraryApplication.Models
{
    public class MediaLibrary : IMediaLibrary
    {
        private ICollection<MediaFile> _mediaFiles { get; }

        private ICollection<IPlayList> _playLists { get; set; }

        private IMediaPlayer player;

        public IEnumerable<MediaFile> MediaFiles => _mediaFiles;

        public IEnumerable<IPlayList> PlayLists => _playLists;

        public MediaLibrary(ICollection<MediaFile> mediaFiles, IMediaPlayer player)
        {
            _mediaFiles = mediaFiles;
            _playLists = new List<IPlayList>();
            this.player = player;
        }

        public void PlayMediaFiles()
        {
            foreach (var file in _mediaFiles)
            {
                file.Play(player);
            }
        }

        public void Play(MediaFile file)
        {
            file.Play(player);
        }

        public void PlayPlaylists()
        {
            if (PlayLists == null)
                throw new ArgumentNullException();

            foreach (var playlist in PlayLists)
            {
                playlist.Play(player);
            }
        }

        public void Add(IPlayList playlist) => _playLists.Add(playlist);

        public void Remove(IPlayList playlist) => _playLists.Remove(playlist);

        public IEnumerable<IPlayList> FindPlaylistBy(Func<IPlayList, bool> func) => _playLists.Where(func);

        public void Add(MediaFile file) => _mediaFiles.Add(file);

        public void Remove(MediaFile file) => _mediaFiles.Add(file);

        public IEnumerable<MediaFile> FindMediaFileBy(Func<MediaFile, bool> func) => _mediaFiles.Where(func);
    }
}
