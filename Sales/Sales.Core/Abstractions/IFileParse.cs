using Sales.Entities.Models;
using System;
using System.Collections.Generic;

namespace Sales.Core.Abstractions
{
    public interface IFileParse
    {
        Tuple<FileData, IEnumerable<FormatLine>> Parse(string path);
    }
}
