using System;

namespace MediaLibraryApplication.Models.Specifications
{
    public class MusicParameters
    {

        private string _genre;
        private DateTime _releaseDate;
        private string _author;

        /// <summary>
        /// Genre of the song
        /// </summary>
        public string Genre
        {
            get => _genre;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();

                _genre = value;
            }
        }
        /// <summary>
        /// Release date of the song
        /// </summary>
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            private set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentNullException();

                _releaseDate = value;
            }
        }
        /// <summary>
        /// Author of the song
        /// </summary>
        public string Author
        {
            get => _author;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();

                _author = value;
            }
        }

        public MusicParameters(string genre, DateTime releaseDate, string author)
        {
            Genre = genre;
            ReleaseDate = releaseDate;
            Author = author;
        }
    }
}
