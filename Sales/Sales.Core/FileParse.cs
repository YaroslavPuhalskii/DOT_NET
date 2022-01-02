using Sales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace Sales.Core
{
    public class FormatLine
    {
        public DateTime DateTime { get; set; }

        public Client Client { get; set; }

        public Manager Manager { get; set; }

        public Product Product { get; set; }

        public decimal Sum { get; set; }
    }

    public class FileParse
    {
        private readonly string _headerDate = ConfigurationManager.AppSettings["headerDate"];

        private readonly string _fileDate = ConfigurationManager.AppSettings["fileDate"];

        private readonly ICollection<FormatLine> _formatLines;

        public FileParse()
        {
            _formatLines = new List<FormatLine>();
        }

        public Tuple<FileData, IEnumerable<FormatLine>> Parse(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
            }

            var header = ParseHeader(path);
            var fileData = ParseFile(path);

            if (header == null || fileData == null)
            {
                throw new ArgumentNullException($"{nameof(header)} or {nameof(fileData)} is null!");
            }

            return new Tuple<FileData, IEnumerable<FormatLine>>(header, fileData);
        }

        private FileData ParseHeader(string path)
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

        private IEnumerable<FormatLine> ParseFile(string path)
        {
            try
            {
                _formatLines.Clear();
                string line;
                using (var reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var formatLine = line.Split(';');

                        _formatLines.Add(new FormatLine
                        {
                            DateTime = DateTime.ParseExact(formatLine[0], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                            Client = new Client() { Name = formatLine[1]},
                            Product = new Product() { Name = formatLine[2]},
                            Sum = decimal.Parse(formatLine[3])
                        });
                    }
                }

                return _formatLines;
            }
            catch(Exception)
            {
                throw new ArgumentException("File can't be exists!");
            }
        }
    }
}
