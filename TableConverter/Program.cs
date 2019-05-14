using System;
using System.Windows.Forms;
using System.Linq;

namespace TableConverter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("GETTING CLIPBOARD");
                string input = Clipboard.GetText();
                Console.WriteLine("PARSING INPUT");
                string output = "\\begin{table}\n\\centering\n\\begin{tabular}{ ";
                if (input.Contains('\r'))
                {
                    input = input.Replace("\r", "");
                }
                if (!input.EndsWith("\n")) input += "\n";
                var rows = input.Split('\n').ToList().Reverse<string>().Skip(1).Reverse<string>();
                var cells = rows.ToList().Select(x => x.Split('\t').ToList());
                Console.WriteLine("CONSTRUCTING OUTPUT");
                output += string.Concat(Enumerable.Repeat("r ", cells.First().Count())) + "}\n";
                output += string.Join("\\\\\n", cells.Select(x => string.Join("&", x.ToArray())).ToArray());
                output += "\\\\\n\\end{tabular}\n\\caption{My table}\n\\label{tab:table}\n\\end{table}\n";
                Console.WriteLine("SETTING CLIPBOARD");
                Clipboard.SetText(output);
                Console.WriteLine("DONE PRESS ANY KEY TO EXIT");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("ERROR PRESS ANY KEY TO EXIT");
                Clipboard.SetText("ERROR");
                Console.ReadKey();
            }
        }
    }
}
