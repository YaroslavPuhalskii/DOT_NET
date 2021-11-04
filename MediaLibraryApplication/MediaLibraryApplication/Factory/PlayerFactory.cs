using MediaLibraryApplication.Core;
using System.Linq;

namespace MediaLibraryApplication.Factory
{
    public static class PlayerFactory
    {
        private static string[] videoExtension { get; } 
            = { ".wmv", ".vob", ".swf", ".mp4", ".mov", ".mkv", ".avi", ".asx", ".asf" };
        private static string[] musicExtension { get; } 
            = { ".wma", ".wav", ".ra", ".mpa", ".mp3", ".mod", ".mid", ".m4a", ".m3u", ".iff", ".ac3", ".amr"};
        private static  string[] photoExtension { get; }
            = { ".jpg", ".jpeg", ".tif", ".tiff", ".png", ".gif", ".bmp", ".dib"};

        public static bool IsVideo(this File file)
        {
            return videoExtension.Contains(file.Format);
        }

        public static bool IsMusic(this File file)
        {
            return musicExtension.Contains(file.Format);
        }

        public static bool IsPhoto(this File file)
        {
            return photoExtension.Contains(file.Format);
        }
    }
}
