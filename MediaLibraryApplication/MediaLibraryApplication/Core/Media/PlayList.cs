using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibraryApplication.Models
{
    public class PlayList : IPlayList
    {
        private int _id;
        private string _name;
        public int Id
        {
            get => _id;
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();

                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();

                _name = value;
            }
        }

        private ICollection<MediaFile> _mediaFiles;

        public IEnumerable<MediaFile> MediaFiles => _mediaFiles;

        public PlayList(int id, string name)
        {
            Id = id;
            Name = name;
            _mediaFiles = new List<MediaFile>();
        }

        public void Play(IMediaPlayer player) => player.Play(this);

        public void Add(MediaFile file) => _mediaFiles.Add(file);

        public void Remove(MediaFile file) => _mediaFiles.Remove(file);

        public IEnumerable<MediaFile> FindBy(Func<MediaFile, bool> func) => _mediaFiles.Where(func);

    }
}
