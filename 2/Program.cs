using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.Red;

Console.WriteLine("Задание 1: LINQ");

Console.ForegroundColor = ConsoleColor.DarkCyan;

Console.WriteLine("-----------------------------");
Console.WriteLine("Дана последовательность непустых строк A . [\"Hello\", \"here\", \"we\", \"are\"].\n" +
    "Получить последовательность символов, каждый элемент которой является начальным символом соответствующей строки из A.\n" +
    "Порядок символов должен быть обратным по отношению к порядку элементов исходной последовательности.");
Console.WriteLine("-----------------------------");

Console.ResetColor();
Console.WriteLine();

string[] s = ["Hello", "here", "we", "are"];
var res = s.Select(x => x.ToCharArray().First()).Reverse();

Console.Write(string.Join(" ", res));
Console.ReadKey();