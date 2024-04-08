using System;
using System.Text;

namespace _6._2_many_cars
{
    internal class Program
    {
        public enum CarsEnum
        {
            Audi,
            Mercedes,
            Opel,
            Porsche,
            BMW,
            VW,
            Ferrari,
            Renault,
            Peugeuo,
            Citroen,
            Maserati,
            Honda,
            Nissan,
            Toyota,
            Subaru,
            Mazda,
            Mitsubishi,
            Bugatti,
            Jaguar,
            LandRover,
            Lamborghini,
            Dodge,
            Ford,
            Lincoln,
            Chevrolet
        }

        public enum BuyersEnum
        {
            Иванов,
            Петров,
            Сидоров,
            Антонов,
            Сергеев,
            Дмитриев,
            Конев,
            Андреев,
            Котов,
            Орехов,
            Окнов,
            Кнопкин,
            Пупкин,
            Серов,
            Носов,
            Вечеров,
            Ковров,
            Васильев,
            Крышин,
            Сачков
        }


        //Добавить в базу рандомный автомобиль
        public static void AddCar(ref List<Car>? cars, ref Queue<int> carIDQueue)
        {
            if (cars is null)
                cars = new List<Car>();

            Random random = new Random();
            Array carsEnum = typeof(CarsEnum).GetEnumValues();

            if (!cars.Any())
            {
                cars.Add(new Car(1, ((CarsEnum)carsEnum.GetValue(random.Next(0, carsEnum.Length))).ToString(), random.Next(0, 50)));
            }
            else
            {
                if (carIDQueue is null || carIDQueue.Count == 0)
                {
                    cars.Add(new Car(cars.Select(x => x.ID).Max() + 1, ((CarsEnum)carsEnum.GetValue(random.Next(0, carsEnum.Length))).ToString(), random.Next(0, 50)));
                }
                else
                {
                    cars.Add(new Car(carIDQueue.Dequeue(), ((CarsEnum)carsEnum.GetValue(random.Next(0, carsEnum.Length))).ToString(), random.Next(0, 50)));
                }
            }
        }

        //Удалить из базы автомобиль
        public static void RemoveCar(ref List<Car>? cars, ref Queue<int>? carIDQueue, List<Buyer>? buyers, int id)
        {
            try
            {
                if (cars is null || cars.Count == 0 || cars.FirstOrDefault(x => x.ID == id) is null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    int tempIndex = buyers.FindIndex(b => b.cars.Contains(id));
                    if (tempIndex > -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"У автомобиля {cars[tempIndex].Name} есть владелец, удаление невозможно!");
                        Console.ResetColor();
                    }
                    else
                    {
                        tempIndex = cars.FindIndex(x => x.ID == id);
                        carIDQueue.Enqueue(id);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Автомобиль {cars[tempIndex].Name} (ID: {id}) удален из списка.");
                        Console.ResetColor();
                        cars.RemoveAt(tempIndex);
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Список пуст или индекс вне диапазона. " + e.Message);
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("Работу с приложением можно продолжить.");
            }
        }


        //Добавить в базу рандомного покупателя
        public static void AddBuyer(ref List<Buyer>? buyers, ref Queue<int> buyerIDQueue)
        {
            if (buyers is null)
                buyers = new List<Buyer>();

            Random random = new Random();
            Array buyersEnum = typeof(BuyersEnum).GetEnumValues();

            {
                if (!buyers.Any())
                {
                    buyers.Add(new Buyer(1, ((BuyersEnum)buyersEnum.GetValue(random.Next(0, buyersEnum.Length))).ToString()));
                }
                else
                {
                    if (buyerIDQueue is null || buyerIDQueue.Count == 0)
                    {
                        buyers.Add(new Buyer(buyers.Select(x => x.ID).Max() + 1, ((BuyersEnum)buyersEnum.GetValue(random.Next(0, buyersEnum.Length))).ToString()));
                    }
                    else
                    {
                        buyers.Add(new Buyer(buyerIDQueue.Dequeue(), ((BuyersEnum)buyersEnum.GetValue(random.Next(0, buyersEnum.Length))).ToString()));
                    }
                }
            }
        }

        //Удалить из базы покупателя
        public static void RemoveBuyer(ref List<Buyer>? buyers, ref Queue<int>? buyerIDQueue, List<Car>? cars, int id)
        {
            try
            {
                if (buyers is null || buyers.Count == 0 || id < 1 || id > buyers.Max(x => x.ID) || buyers.FirstOrDefault(x => x.ID == id) is null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    int tempIndex = buyers.FindIndex(x => x.ID == id);
                    List<int> carID = buyers[tempIndex].cars;
                    if (buyers[tempIndex].cars.Count > 0)
                    {
                        //var car = cars.First(x => x.ID == carID);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Удаление невозможно! {buyers[tempIndex].Name} является владельцем: ");
                        foreach (var car in carID)
                            Console.WriteLine($"{cars.First(x => x.ID == car).Name}, {cars.First(x => x.ID == car).Age}лет (ID: {cars.First(x => x.ID == car).ID})\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        tempIndex = buyers.FindIndex(x => x.ID == id);
                        buyerIDQueue.Enqueue(id);

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Покупатель {buyers[tempIndex].Name} (ID: {id}) удален из списка.");
                        Console.ResetColor();
                        buyers.RemoveAt(tempIndex);
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Список пуст или индекс вне диапазона. " + e.Message);
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("Работу с приложением можно продолжить.");
            }
        }


        //Печать списка автомобилей
        public static void PrintCars(List<Car>? cars)
        {
            try
            {
                if (cars is null)
                    throw new NullReferenceException();
                else
                {
                    if (cars.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------Список автомобилей-------");
                        Console.ResetColor();
                        foreach (var car in cars)
                        {
                            Console.WriteLine($"ID: {car.ID}, Name: {car.Name}, Age: {car.Age}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("----Список автомобилей пуст----");
                        Console.ResetColor();
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Список равен null! " + e.Message);
            }
        }

        //Печать списка покупателей
        public static void PrintBuyers(List<Buyer>? buyers, List<Car> cars)
        {
            try
            {
                if (buyers is null)
                    throw new NullReferenceException();
                else
                {
                    if (buyers.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------Список покупателей-------");
                        Console.ResetColor();
                        var b = buyers.OrderBy(x => x.Name).GroupBy(x => x.cars.Count > 0).OrderByDescending(g => g.Key).SelectMany(g => g.OrderBy(x => x.Name)).ToList();
                        foreach (var buyer in b)
                        {
                            Console.Write($"ID: {buyer.ID} Name: {buyer.Name}");
                            if (buyer.cars.Count > 0)
                            {
                                var car = buyer.cars;
                                Console.Write($", владеет :");
                                foreach (int i in car)
                                    Console.Write($"\t{cars.First(x => x.ID == i).Name}, возрастом {cars.First(x => x.ID == i).Age} лет (ID: {cars.First(x => x.ID == i).ID})\n\t\t\t");
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                            Console.CursorLeft = 0;
                        }
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("----Список покупателей пуст----");
                        Console.ResetColor();
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Список равен null! " + e.Message);
            }
        }


        //Операция покупки автомобиля
        public static void BuyCar(ref List<Buyer> buyers, List<Car>? cars, int b, int c)
        {
            try
            {
                if (buyers == null || cars == null)
                    throw new ArgumentException();
                else
                {
                    if (!buyers.Any())
                    {
                        Console.WriteLine("Покупатели отсутствуют.");
                    }
                    else if (!cars.Any())
                    {
                        Console.WriteLine("Автомобили отсутствуют.");
                    }
                    else if (buyers.FirstOrDefault(x => x.ID == b) is null || cars.FirstOrDefault(x => x.ID == c) is null)
                    {
                        Console.WriteLine("Некооректный ID, покупка невозможна. Попробуйте заново.");
                    }
                    else
                    {
                        var buyerTemp = buyers.FirstOrDefault(x => x.ID == b);
                        int index = buyers.FindIndex(x => x.ID == b);
                        var carTemp = cars.First(x => x.ID == c);

                        if (buyers.FirstOrDefault(x => x.cars.Contains(c)) is not null)
                        {
                            Console.WriteLine($"У автомобиля уже есть другой собственник.");

                        }
                        else
                        {
                            //int index = buyers.FindIndex(x => x.ID == b);
                            //buyerTemp = buyers[index];
                            buyers[index].cars.Add(c);
                            Console.WriteLine($"Сделка состоялась. {buyers[index].Name} купил {carTemp.Name}, возрастом {carTemp.Age} лет, ID: {carTemp.ID}.");
                        }
                    }

                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Неверный тип входных данных: список не может быть null! " + e.Message);
            }
        }

        //Операция продажи автомобиля
        public static void SellCar(ref List<Buyer> buyers, List<Car> cars, int idB, int idC)
        {
            try
            {
                if (buyers == null || cars == null || buyers.FirstOrDefault(x => x.ID == idB) is null || !buyers.FirstOrDefault(x => x.ID == idB).cars.Contains(idC) )
                    throw new ArgumentException();
                else
                {
                    if (!buyers.Any())
                    {
                        Console.WriteLine("Покупатели отсутствуют.");
                    }
                    else
                    {
                        var buyerTemp = buyers.FirstOrDefault(x => x.ID == idB);
                        int index = buyers.FindIndex(x => x.ID == idB);
                        if (buyerTemp is not null)
                        {
                            if (buyerTemp.cars.Count == 0)
                            {
                                Console.WriteLine($"{buyerTemp.Name} не владеет автомобилем!");
                            }
                            else
                            {
                                buyers[index].cars.Remove(idC);
                                Console.WriteLine($"{buyers[index].Name} продал {cars.First(x=>x.ID==idC).Name}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Такой покупатель не найден.");
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Список пуст или индекс вне диапазона. " + e.Message);
            }
            finally
            {
                Console.WriteLine("Работу с приложением можно продолжить.");
            }
        }


        public static ConsoleKeyInfo PressKey()
        {
            int cursorLeft = Console.CursorLeft;
            ConsoleKeyInfo key = Console.ReadKey();
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            return key;
        }

        public static void PrintGuide()
        {
            const string Guide =
                "1 - Добавить 5 автомобилей\t2 - Удалить 1 автомобиль\t3 - Печать списка автомобилей\n" +
                "4 - Добавить 5 покупателей\t5 - Удалить 1 покупателя\t6 - Печать списка покупателей\n" +
                "7 - Произвести покупку авто\t8 - Произвести продажу авто\n" +
                "0 - Инструкция\nESC - выход";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Guide);
            Console.ResetColor();
        }



        static void Main(string[] args)
        {
            Queue<int> carIDQueue = new Queue<int>(), buyerIDQueue = new Queue<int>();

            List<Car> cars = new List<Car>();
            List<Buyer> buyers = new List<Buyer>();

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            ConsoleKeyInfo key = new ConsoleKeyInfo();



            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задание 6.2 * \nОдин покупатель может купить много машин.");
            Console.ResetColor();

            PrintGuide();

            do
            {
                key = PressKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        {
                            for (int i = 0; i < 5; i++)
                                AddCar(ref cars, ref carIDQueue);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("5 авто успешно добавлены!");
                            Console.ResetColor();
                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            int id = -1;
                            Console.Write("Введите ID автомобиля для удаления: ");
                            do
                            {

                                string? input = Console.ReadLine();
                                if (int.TryParse(input, out int result))
                                {
                                    id = result;
                                    RemoveCar(ref cars, ref carIDQueue, buyers, id);
                                }
                                else
                                {
                                    Console.Write("Некорректный ввод ID, повторите.");
                                }
                            } while (id < 0);
                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            PrintCars(cars);
                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            for (int i = 0; i < 5; i++)
                                AddBuyer(ref buyers, ref buyerIDQueue);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("5 покупателей успешно добавлены!");
                            Console.ResetColor();
                            break;
                        }

                    case ConsoleKey.D5:
                        {
                            int id = -1;
                            Console.Write("Введите ID покупателя для удаления: ");
                            do
                            {

                                string? input = Console.ReadLine();
                                if (int.TryParse(input, out int result))
                                {
                                    id = result;
                                    RemoveBuyer(ref buyers, ref buyerIDQueue, cars, id);
                                }
                                else
                                {
                                    Console.Write("Некорректный ввод ID, повторите.");
                                }
                            } while (id < 0);
                            break;
                        }

                    case ConsoleKey.D6:
                        {
                            PrintBuyers(buyers, cars);
                            break;
                        }

                    case ConsoleKey.D7:
                        {
                            int idB = -1, idC = -1;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Введите ID покупателя : ");
                            Console.ResetColor();
                            do
                            {

                                string? input = Console.ReadLine();
                                if (int.TryParse(input, out int result))
                                {
                                    idB = result;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Некорректный ввод ID, повторите: ");
                                    Console.ResetColor();
                                }
                            } while (idB < 0);

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Введите ID автомобиля: ");
                            Console.ResetColor();
                            do
                            {

                                string? input = Console.ReadLine();
                                if (int.TryParse(input, out int result))
                                {
                                    idC = result;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Некорректный ввод ID, повторите: ");
                                    Console.ResetColor();
                                }
                            } while (idC < 0);

                            BuyCar(ref buyers, cars, idB, idC);

                            break;
                        }

                    case ConsoleKey.D8:
                        {
                            int idB = -1, idC = -1;
                            string? input;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Введите ID покупателя (который в роли продавца) : ");
                            Console.ResetColor();
                            do
                            {

                                input = Console.ReadLine();
                                if (int.TryParse(input, out int result))
                                {
                                    idB = result;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Некорректный ввод ID, повторите: ");
                                    Console.ResetColor();
                                }
                            } while (idB < 0);

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Введите ID продаваемого им автомобиля : ");
                            Console.ResetColor();
                            do
                            {

                                input = Console.ReadLine();
                                if (int.TryParse(input, out int result))
                                {
                                    idC = result;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Некорректный ввод ID, повторите: ");
                                    Console.ResetColor();
                                }
                            } while (idC < 0);

                            SellCar(ref buyers, cars, idB, idC);
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
                                Thread.Sleep(15);
                            }
                            Console.ResetColor();
                            Thread.Sleep(200);
                            Console.WriteLine();
                            Environment.Exit(0);
                            break;
                        }

                    default: break;
                }

            } while (true);
        }
    }
}
