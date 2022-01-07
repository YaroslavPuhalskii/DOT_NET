using Sales.Core.Abstractions;
using Sales.Entities.Models;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace Sales.Core
{
    public class FormatLine
    {
        public DateTime DateTime { get; set; }

        public Client Client { get; set; }

        public Product Product { get; set; }

        public decimal Sum { get; set; }
    }

    public class FileParser : IFileParser
    {
        private readonly string _headerDate = ConfigurationManager.AppSettings["headerDate"];

        private readonly string _fileDate = ConfigurationManager.AppSettings["fileDate"];

        public FileData ParseHeader(string path)
        {
            var headerLine = Path.GetFileNameWithoutExtension(path);

            if (string.IsNullOrEmpty(headerLine))
            {
                throw new ArgumentException(nameof(headerLine));
            }

            var header = headerLine.Split('_');

            if (header.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(header));
            }

            return new FileData()
            {
                Manager = new Manager() { Name = header[0] },
                DateCreate = DateTime.ParseExact(header[1], _headerDate, CultureInfo.InvariantCulture)
            };
        }

        public FormatLine ParseLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new ArgumentNullException();
            }

            var formatLine = line.Split(';');

            if (formatLine.Length != 4)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new FormatLine()
            {
                DateTime = DateTime.ParseExact(formatLine[0], _fileDate, CultureInfo.InvariantCulture),
                Client = new Client() { Name = formatLine[1] },
                Product = new Product() { Name = formatLine[2] },
                Sum = decimal.Parse(formatLine[3])
            };
        }
    }
}
