using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibraryApplication.Core
{
    public interface IMediaFileFunction
    {
        void AddMediaFile(File file);
        void RemoveMediaFile(File file);
        IEnumerable<File> FindMediaFile(string name);
        IEnumerable<File> GetMediaFiles();
    }
}
