using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.Red;

Console.WriteLine("Задание 1: LINQ");

Console.ForegroundColor = ConsoleColor.DarkCyan;

Console.WriteLine("-----------------------------");
Console.WriteLine("Дана целочисленная последовательность [-10, 22, 13, 43, -5, -12, 100].\n" +
    "Извлечь из нее все четные отрицательные числа, поменяв порядок извлеченных чисел на обратный.");
Console.WriteLine("-----------------------------");

Console.ResetColor();
Console.WriteLine();

int[] a = [-10, 22, 13, 43, -5, -12, 100];
var res = a.Where(x => x < 0).Reverse();

Console.Write(string.Join(" ", res));
Console.ReadKey();