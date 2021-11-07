using MediaLibraryApplication.Core.Media;
using System;
using System.Collections.Generic;

namespace MediaLibraryApplication.Abstractions
{
    public interface IPlayList
    {
        IEnumerable<MediaFile> MediaFiles { get; }

        void Add(MediaFile file);

        void Remove(MediaFile file);

        IEnumerable<MediaFile> FindBy(Func<MediaFile, bool> predicate);
    }
}
