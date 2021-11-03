using MediaLibraryApplication.Core.PlayList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibraryApplication.Core.MediaLibrary
{
    public interface IMediaLibrary : IPlayListFunction, IMediaFileFunction
    {
    }
}
