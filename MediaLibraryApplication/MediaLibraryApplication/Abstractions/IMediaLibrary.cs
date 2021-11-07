using MediaLibraryApplication.Core.Media;
using MediaLibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibraryApplication.Abstractions
{
    public interface IMediaLibrary
    {
        PlayList DefaultPlaylist { get; }

        ICollection<IPlayList> PlayLists { get; }

        void Play(MediaFile file);

        void PlayPlaylist();
    }
}
