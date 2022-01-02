using Sales.Entities.Models;
using System.Collections.Generic;

namespace Sales.Core.Abstractions
{
    public interface IFileLoader
    {
        void Add(FileData fileData, IEnumerable<FormatLine> formatLines);
    }
}
