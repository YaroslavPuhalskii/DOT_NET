using NLog;
using Sales.Core.Abstractions;
using Sales.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sales.Core
{
    public class FileReader : IFileReader
    {
        private ICollection<FormatLine> _formatLines;

        private readonly IFileParser _parser;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public FileReader(IFileParser parser)
        {
            _parser = parser;
        }

        public Tuple<FileData, IEnumerable<FormatLine>> Read(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                logger.Info($"Begin read: {path}");

                _formatLines = new List<FormatLine>();

                var header = _parser.ParseHeader(path);

                string line;
                using (var reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        _formatLines.Add(_parser.ParseLine(line));
                    }
                }

                return new Tuple<FileData, IEnumerable<FormatLine>>(header, _formatLines);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
