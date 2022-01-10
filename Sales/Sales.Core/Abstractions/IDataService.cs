using Sales.Entities.Models;
using System.Collections.Generic;

namespace Sales.Core.Abstractions
{
    public interface IDataService
    {
        void Save(FileData fileData, IEnumerable<FormatLine> formatLines);
    }
}
