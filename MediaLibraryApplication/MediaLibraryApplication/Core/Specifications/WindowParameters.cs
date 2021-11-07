using System;

namespace MediaLibraryApplication.Models.Specifications
{
    public class WindowParameters
    {
        private int _width;
        private int _height;

        public int Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();

                _width = value;
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();

                _height = value;
            }
        }
        public WindowParameters(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
