using MediaLibraryApplication.Core.PlayList;
using System;

namespace MediaLibraryApplication.Core
{
    public abstract class File
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

        public File(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void Play(File file);
        public abstract void Play(IPlayList playList);
    }
}
