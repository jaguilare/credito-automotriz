using CreditoAutomotriz.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CreditoAutomotriz.Repository.Implementation
{
    public class ArchivosRepository<T> : IArchivosRepository<T>
    {
        public List<T> CargarCsv<T>(string fileName, ICsvMapping<T> mapping)
        {
            List<T> records = new List<T>();

            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
                CsvParser<T> csvParser = new CsvParser<T>(csvParserOptions, mapping);
                records = csvParser.ReadFromFile(@filePath, Encoding.GetEncoding("iso-8859-1"))
                    .Where(x => x.IsValid)
                    .Select(x => x.Result)
                    .ToList();
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error al cargar el archivo {fileName}, => {exc}");
                throw new Exception($"Error al cargar el archivo {fileName}");
            }
            return records;
        }
    }
}
