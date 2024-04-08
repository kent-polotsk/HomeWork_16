using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.Red;

Console.WriteLine("Задание 3*: LINQ");

Console.ForegroundColor = ConsoleColor.DarkCyan;

Console.WriteLine("-----------------------------");
Console.WriteLine("Дана последовательность-> [11,-20,-5,4,5,8,-1,9,2,0,-11,15,3,-3,4,0,20,-40,-5,-11,-18,10]\n" +
    " a) Извлечь из нее все положительные числа, сохранив их исходный порядок следования.\n" +
    " b) Извлечь из нее все нечетные числа, сохранив их исходный порядок следования\n" +
    "    и удалив все вхождения повторяющихся элементов, кроме первых.\n" +
    " c) Извлечь из нее все четные отрицательные числа, поменяв порядок извлеченных чисел на обратный.\n" +
    " d) Извлечь из нее все положительные двузначные числа, отсортировав их по возрастанию");
Console.WriteLine("-----------------------------");

Console.ResetColor();

int[] list = [11, -20, -5, 4, 5, 8, -1, 9, 2, 0, -11, 15, 3, -3, 4, 0, 20, -40, -5, -11, -18, 10];


var arrPos = list.Where(x => x > 0);
Console.WriteLine("\n-------------a---------------");
Console.Write(string.Join(" ", arrPos));



var arrNeg = list.Where(x => ((x % 2) != 0)).Distinct();
Console.WriteLine("\n-------------b---------------");
Console.Write(string.Join(" ", arrNeg));



var arrRev = list.Where(x => ( x < 0)).Where(x => (x % 2) == 0).Reverse();
Console.WriteLine("\n-------------c---------------");
Console.Write(string.Join(" ", arrRev));



var arrOrd = list.Where(x => (x >= 10)).Order();
Console.WriteLine("\n-------------d---------------");
Console.Write(string.Join(" ", arrOrd));


Console.ReadKey();