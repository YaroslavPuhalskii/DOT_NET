using Sales.Entities.Models;
using System.Collections.Generic;

namespace Sales.Core.Abstractions
{
    public interface IDataService
    {
        bool Save(FileData fileData, IEnumerable<FormatLine> formatLines);
    }
}
