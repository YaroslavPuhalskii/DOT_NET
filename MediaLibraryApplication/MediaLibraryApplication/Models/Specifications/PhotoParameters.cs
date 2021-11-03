using System;

namespace MediaLibraryApplication.Models.Specifications
{
    public class PhotoParameters
    {
        private string _photographer;
        public string Photographer
        {
            get => _photographer;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                _photographer = value;
            }
        }

        public WindowParameters Window { get; set; }

        public PhotoParameters(WindowParameters window, string photograher)
        {
            Window = window;
            Photographer = photograher;
        }
    }
}
