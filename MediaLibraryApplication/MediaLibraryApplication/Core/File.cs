using MediaLibraryApplication.Core.PlayList;
using System;

namespace MediaLibraryApplication.Core
{
    public abstract class File : IFile
    {
        private int _id;
        private string _name;
        private string _format;
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
        public string Format
        {
            get => _format;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();

                _format = value;
            }
        }

        public File(int id, string name, string format)
        {
            Id = id;
            Name = name;
            Format = format;
        }
    }
}
