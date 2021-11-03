using System;

namespace MediaLibraryApplication.Models.Specifications
{
    public class VideoParameters
    {
        private int _quality;
        public int Quality
        {
            get => _quality;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();

                _quality = value;
            }
        }

        public WindowParameters Window { get; set; }

        public VideoParameters(WindowParameters window, int quality)
        {
            Window = window;
            Quality = quality;
        }
    }
}
