using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.PlayList;
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

        private ICollection<File> MediaFiles { get; set; }

        public PlayList(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public IEnumerable<File> GetMediaFiles()
        {
            return MediaFiles;
        }

        public void AddMediaFile(File file)
        {
            MediaFiles.Add(file);
        }

        public void RemoveMediaFile(File file)
        {
            MediaFiles.Remove(file);
        }

        public IEnumerable<File> FindMediaFile(string name)
        {
            return MediaFiles.Where(x => x.Name == name);
        }
    }
}
