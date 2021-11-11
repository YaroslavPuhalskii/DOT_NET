using MediaLibraryApplication.Core.Media;
using System;
using System.Collections.Generic;

namespace MediaLibraryApplication.Abstractions
{
    public interface IMediaLibrary
    {
        void Add(IPlayList playlist);

        void Remove(IPlayList playlist);

        void Add(MediaFile file);

        void Remove(MediaFile file);

        IEnumerable<MediaFile> MediaFiles { get; }

        IEnumerable<IPlayList> PlayLists { get; }

        IEnumerable<MediaFile> FindMediaFileBy(Func<MediaFile, bool> func);

        IEnumerable<IPlayList> FindPlaylistBy(Func<IPlayList, bool> func);

        void Play(MediaFile media);

        void PlayMediaFiles();

        void PlayPlaylists();
    }
}
