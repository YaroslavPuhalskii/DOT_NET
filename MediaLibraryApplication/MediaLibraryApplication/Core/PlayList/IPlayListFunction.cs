using System.Collections.Generic;

namespace MediaLibraryApplication.Core.PlayList
{
    public interface IPlayListFunction
    {
        IEnumerable<File> GetMediaFiles();
        void AddMediaFile(File file);
        void RemoveMediaFile(File file);
        IEnumerable<File> FindMediaFile(string name);
    }
}
