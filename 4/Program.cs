using _4;
using System.Text;


internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine("Задание 4-5: LINQ");

        Console.ForegroundColor = ConsoleColor.DarkCyan;

        Console.WriteLine("-----------------------------");
        Console.WriteLine("Исходная последовательность содержит сведения о клиентах фитнес - центра.\n" +
            "Каждый элемент последовательности включает следующие целочисленные поля:\n" +
            "< Год > < Номер месяца > < Продолжительность занятий(в часах) > < Код клиента >\n" +
            "Определить год, в котором суммарная продолжительность занятий всех клиентов была наибольшей\n" +
            "и вывести этот год и наибольшую суммарную продолжительность.\n" +
            "Если таких годов было несколько, то вывести наименьший из них. ");
        Console.WriteLine("-----------------------------");

        Console.WriteLine("Создать свой метод LINQ, Median, метод должен быть методом расширения,\n" +
            "и он должен возвращать элемент перечисления находящийся в середине.");   
            Console.WriteLine("-----------------------------");
        Console.ResetColor();

        PrintGuide();

        Random random = new();
        List<Client> clients = new List<Client>(1);

        ConsoleKeyInfo key;

        do
        {
            key = PressKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    {
                        for (int i = 0; i < 10; i++)
                            AddClient(ref clients);

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Добавлено 10 записей");
                        Console.ResetColor();
                        break;
                    }
                case ConsoleKey.D2:
                    {
                            AddClient(ref clients);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Добавлена 1 запись");
                        Console.ResetColor();
                        break;
                    }

                case ConsoleKey.D3:
                    {
                        PrintClients(clients);
                        break;
                    }

                case ConsoleKey.D4:
                    {
                        if (clients != null && clients.Any())
                        {
                            var res = clients.GroupBy(x => x.Year)
                                .Select(x => new { year = x.Key, hours = x.Sum(x => x.Hours) })
                                .OrderByDescending(x => x.hours)
                                .ThenBy(x => x.year)
                                .FirstOrDefault();
                            if (res != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"Наибольшая суммарная продолжительность занятий {res.hours} часов была в {res.year} году");
                                Console.ResetColor();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Записи отсутствуют");
                            break;
                        }
                    }

                case ConsoleKey.D5:
                    {
                        if (clients == null || !clients.Any())
                        {
                            Console.WriteLine("Список клиентов пуст.");
                        }
                        else
                        {
                            var m = clients.MedianElement();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Элемент перечисления находящийся в середине : ID: {m.ID}, год: {m.Year}, месяц: {m.Month}, количество часов: {m.Hours}");
                            Console.ResetColor();
                        }
                        break;
                    }

                case ConsoleKey.D0:
                    {
                        PrintGuide();
                        break;
                    }

                case ConsoleKey.Escape:
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine("1");
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string bye = "Работа приложения завершена...";
                        for (int i = 0; i < bye.Length; i++)
                        {
                            Console.Write(bye[i]);
                            Thread.Sleep(20);
                        }
                        Console.ResetColor();
                        Thread.Sleep(300);
                        Console.WriteLine();
                        Environment.Exit(0);
                        break;
                    }


                default: break;
            }
        } while (true);


        void PrintClients(List<Client>? clients)
        {
            if (clients == null || clients.Count == 0)
            {
                Console.WriteLine("Список клиентов пуст.");
            }
            else
            {
                foreach (Client client in clients)
                    Console.WriteLine($"ID: {client.ID}, год: {client.Year}, месяц: {client.Month}, количество часов: {client.Hours}");
            }
        }

        void PrintGuide()
        {
            const string Guide =
                "1 - Добавить 10  записей\t2 - Добавить 1  запись\n" +
                "3 - Печать записей\t\t4 - Вывести ответ\n" +
                "5 - Медианный элемент\t\t0 - Инструкция\n" +
                "ESC - выход";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Guide);
            Console.ResetColor();
        }


        ConsoleKeyInfo PressKey()
        {
            int cursorLeft = Console.CursorLeft;
            ConsoleKeyInfo key = Console.ReadKey();
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            return key;
        }

        void AddClient(ref List<Client>? clients)
        {
            if (clients == null)
            {
                clients = [new Client(1)];
            }
            else if (clients.Count == 0)
            {
                clients.Add(new Client(1));
            }
            else
            {
                clients.Add(new Client(clients.Select(x => x.ID).Max() + 1));
            }
        }
    }
}

public class Client
{
    public int Year { get; }
    public int Month { get; }
    public int Hours { get; }
    public int ID { get; init; }

    Random Random = new Random();

    public Client(int id)
    {
        Year = Random.Next(2010, DateTime.Now.Year + 1);

        if (Year < DateTime.Now.Year)
        {
            Month = Random.Next(1, 13);
        }
        else
        {
            Month = Random.Next(1, DateTime.Now.Month + 1);
        }

        Hours = Random.Next(0, 20);
        ID = id;
    }
};