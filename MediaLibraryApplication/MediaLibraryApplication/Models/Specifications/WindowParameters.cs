using System;

namespace MediaLibraryApplication.Models.Specifications
{
    public class WindowParameters
    {
        private int _width;
        private int _height;
        /// <summary>
        /// Window width
        /// </summary>
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
        /// <summary>
        /// Window height
        /// </summary>
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
