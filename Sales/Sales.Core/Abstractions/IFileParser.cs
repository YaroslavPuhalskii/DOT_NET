using Sales.Entities.Models;

namespace Sales.Core.Abstractions
{
    public interface IFileParser
    {
        FileData ParseHeader(string path);

        FormatLine ParseLine(string line);
    }
}
