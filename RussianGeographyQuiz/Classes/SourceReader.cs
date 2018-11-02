using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RussianGeographyQuiz.Classes
{
    class SourceReader
    {
        public List<TerritorialObject> Read(string FileName)
        {
            //Чтение из файла:
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseFolder, FileName);
            string unformattedSource;
            using (FileStream fileStream = File.OpenRead(filePath))
            using (TextReader reader = new StreamReader(fileStream))
            {
                unformattedSource = reader.ReadToEnd();
            }
            var A = unformattedSource.Split(';');
            var source = new List<TerritorialObject>();
            foreach (var item in A)
            {
                var sourceItemString = item.Split(',');
                var sourceItem = new TerritorialObject(sourceItemString[0], sourceItemString[1]);
                source.Add(sourceItem);
            }
            return source;
        }
    }
}
