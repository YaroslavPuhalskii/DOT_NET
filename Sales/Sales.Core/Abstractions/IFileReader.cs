using Sales.Entities.Models;
using System;
using System.Collections.Generic;

namespace Sales.Core.Abstractions
{
    public interface IFileReader
    {
        Tuple<FileData, IEnumerable<FormatLine>> Read(string path);
    }
}
