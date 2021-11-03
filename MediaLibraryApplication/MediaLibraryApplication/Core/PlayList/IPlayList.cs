using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibraryApplication.Core.PlayList
{
    public interface IPlayList
    {
        int Id { get; }
        string Name { get; set; }
    }
}
