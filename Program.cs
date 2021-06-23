using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirtyWordProcessor
{
    class Program
    {
        const string ROOT = @"C:\Git\E\hallserver\room\config\txt\";

        const char SPLIT = '|';

        static void Main(string[] args)
        {
            /*origin与append以及最终产出的dirtyWords2中均为A|B|C|形式的数据*/
            var origin = GetContent("dirtyWords");
            var append = GetContent("append");

            var temp = origin.ToList();
            temp.AddRange(append.ToList());
            temp.Distinct();

            temp.Sort((word1, word2) =>
            {
                return word2.Length - word1.Length;
            });

            SetContent("dirtyWords_new", Collection2string(temp));

            SetContent("delta", Collection2string(temp.Except(origin)));
        }

        static string Collection2string(IEnumerable<string> list)
        {
            var sb = new StringBuilder();

            foreach (var word in list)
            {
                if (word.Length == 0)
                    continue;

                sb.Append(word);
                sb.Append(SPLIT);
            }

            return sb.ToString();
        }

        static string[] GetContent(string fileName)
        {
            var rawContent = File.ReadAllText(GetFilePath(fileName));

            return rawContent.Split(SPLIT);
        }

        static void SetContent(string fileName, string content)
        {
            File.WriteAllText(GetFilePath(fileName), content);
        }

        static string GetFilePath(string fileName)
        {
            return string.Format("{0}{1}.txt", ROOT, fileName);
        }
    }
}

