using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace scheduler
{
    class Business
    {
        public string Name { get; set; }   // название дела
        private string PrivateTime; // время выполнения дела
        public string Time
        {
            set                          // проверка правильного ввода времени
            {
                DateTime dt;
                while (!DateTime.TryParseExact(value, "t", null, DateTimeStyles.None, out dt))
                {
                    Console.Write("\n\n  Вы использовали некорректный формат времени!" +
                                    "\n пожалуйста используйте двоеточие, как разделитель, " +
                                    "\n и не пропускайте нули (например: 06:02 или 21:50)" +
                                   "\n\nПОВТОРИТЕ ВВОД ВРЕМЕНИ: ");

                    value = Console.ReadLine();
                }
                PrivateTime = Convert.ToString(dt.ToString("hh:mm"));
            }
            get
            { return PrivateTime; }
        }
        public string Important { get; set; } // важность дела

    }

    class Program
    {
        static ConsoleKey choice;
        static int once = 0;
        static int sch;
        static void Main()
        {
            List<Business> businesses = new List<Business>();   // инициализация списка объектов "дел"


            void All_things()   //  метод для вывода полей объектов из списка List в памяти
            {
                if (businesses.Count == 0)
                { Console.WriteLine("\n\n  В памяти нет списка дел... \n"); }
                else
                    for (int i = 0; i < businesses.Count; i++)
                    {
                        sch = i + 1;
                        Console.WriteLine(" дело № " + sch +
                                          "\n    название: " + businesses[i].Name +
                                          "\n    время:    " + businesses[i].Time +
                                          "\n    важность: " + businesses[i].Important);
                        Console.WriteLine("------------------");
                    }
            }

            do
            {
                do
                {
                    if (once == 0)               // вывод меню
                    {
                        Console.WriteLine("\n      Вас приветствует планировщик заданий! " +
                                          "\n             С Новым Годом Вас ;)\n ");
                    }
                    Console.WriteLine("\n Выберите необходимое действие, введя цифру: \n"
                                       + "   1 - посмотреть весь список дел\n"
                                       + "   2 - добавить пункт в список\n"
                                       + "   3 - удалить пункт из списка\n"
                                       + "   4 - редактировать список\n"
                                       + "   5 - сохранить список в файл\n"
                                       + "   6 - загрузить список из файла\n"
                                       + " Для выхода из программы нажмите Esc.");
                    if (once == 0)
                    {
                        Console.WriteLine("\n *** для облегчения ввода данных при проверке программы - выберите пункт 6");
                        once++;
                    }

                    choice = Console.ReadKey(true).Key;

                    if (choice == ConsoleKey.D1)        //    Посмотреть весь список дел   ******* 1 ******* 
                    {
                        if (businesses.Count != 0) Console.WriteLine("\n\n\n  Вот список всех дел в памяти: \n");

                        All_things();
                    }


                    else if (choice == ConsoleKey.D2)   //    добавить пункт в список      ******* 2 *******
                    {
                        Console.Clear();
                        Console.Write("\n\n   Введите название дела: ");
                        businesses.Add(new Business() { Name = Console.ReadLine() });

                        Console.Write("   Введите время выполнения дела в формате чч:мм (например: 6:05): ");
                        businesses[^1].Time = Console.ReadLine();

                        Console.Write("   важность дела (важное- 1, неважное- 0): ");
                        businesses[^1].Important = Console.ReadLine();
                    }


                    else if (choice == ConsoleKey.D3)   //      удалить пункт из списка    ******* 3 *******
                    {
                        if (businesses.Count == 0)     //проверка корректности ввода номера дела
                        { Console.WriteLine("\n в памяти нет списка, введите его или загрузите из файла.."); }
                        else
                        {
                            Console.WriteLine("\n\n  Вот список всех дел:\n ");
                            All_things();

                            Console.Write("\n    Какой пункт удалить?: ");
                            int index = Convert.ToInt32(Console.ReadLine());
                            if (index > businesses.Count | index < 0 | index == 0)
                                do
                                {
                                    Console.WriteLine("\n\n  нет такого пункта...сорян\n  попробуйте еще.\n ");
                                    Console.Write("\n    Какой пункт удалить?: ");
                                    index = Convert.ToInt32(Console.ReadLine());
                                }
                                while (index > businesses.Count | index < 0 | index == 0);

                            businesses.RemoveAt(index - 1);
                            Console.Write("\n\n пункт удален...\n");
                        }
                    }

                    else if (choice == ConsoleKey.D4)   //   редактировать список          ******* 4 *******
                    {
                        if (businesses.Count == 0)      //проверка корректности ввода номера дела
                        { Console.WriteLine("\n\n в памяти нет списка, введите его или загрузите из файла.."); }
                        else
                        {
                            Console.WriteLine("\n\n  Вот список всех дел:\n ");
                            All_things();

                            Console.Write("\n    Какой пункт будете редактировать?: ");
                            int index = Convert.ToInt32(Console.ReadLine());
                            if (index > businesses.Count | index < 0 | index == 0)
                                do
                                {
                                    Console.WriteLine("\n\n  нет такого пункта...сорян\n  попробуйте еще.\n ");
                                    Console.Write("\n    Какой пункт будете редактировать?: ");
                                    index = Convert.ToInt32(Console.ReadLine());
                                }
                                while (index > businesses.Count | index < 0 | index == 0);

                            Console.Write("\n\n   Введите новое название дела №" + index + " : ");
                            businesses.Insert(index, new Business() { Name = Console.ReadLine() });

                            Console.Write("   Введите время выполнения дела в формате чч:мм (например: 6:05): ");
                            businesses[index].Time = Console.ReadLine();

                            Console.Write("   важность дела (важное- 1, неважное- 0): ");
                            businesses[index].Important = Console.ReadLine();

                            businesses.RemoveAt(index - 1);

                            Console.WriteLine("\n\n    Теперь список дел выглядит вот так:\n");
                            All_things();
                        }
                    }

                    else if (choice == ConsoleKey.D5)   //     сохранить список в файл    ******* 5 *******
                    {
                        using StreamWriter WriteRem = File.CreateText("Reminders.txt");

                        for (int i = 0; i < businesses.Count; i++)
                        {
                            WriteRem.WriteLine(businesses[i].Name);
                            WriteRem.WriteLine(businesses[i].Time);
                            WriteRem.WriteLine(businesses[i].Important);
                        }
                        Console.WriteLine("\n\n   Список записан в файл. \n");
                    }


                    else if (choice == ConsoleKey.D6)   //     загрузка списка из файла      ******* 6 *******
                    {
                        try   // проверка возможности открытия файла
                        {
                            StreamReader ReadRem = File.OpenText("Reminders.txt");
                            Console.WriteLine("\n\n  Список дел, загруженных из файла:\n ");
                            sch = 0;
                            string st;
                            if (businesses.Count != 0) businesses.Clear(); // очистка списка в памяти, если он не пуст
                            while (true) // заполнение списка из файла построчно до конца файла
                            {
                                st = ReadRem.ReadLine();
                                if (st == null & sch == 0)
                                { Console.WriteLine("        Файл пуст..."); Console.ReadLine(); break; }
                                if (st == null) break;

                                string name;
                                string time;
                                string important;

                                name = st;
                                st = ReadRem.ReadLine();
                                time = st;
                                st = ReadRem.ReadLine();
                                important = st;
                                businesses.Add(new Business() { Name = name, Time = time, Important = important });
                                sch++;
                            }
                            ReadRem.Close();
                            All_things();
                        }
                        catch (IOException e)   // обработка исключения, вывод сообщения об ошибке
                        {
                            Console.WriteLine("\n   Файл не может быть прочитан\n");
                            Console.WriteLine(e.Message);
                        }
                    }

                    //обработка ввода
                    while (choice != ConsoleKey.Y && choice != ConsoleKey.N && (choice != ConsoleKey.Escape))
                    {
                        Console.Write("\n   Продолжить? Y/N  ");
                        choice = Console.ReadKey().Key;
                        if (choice != ConsoleKey.Y && choice != ConsoleKey.N && (choice != ConsoleKey.Escape))
                            Console.WriteLine("\n\n   Ошибка при вводе (вводите только Y,N или Esc) ");
                    }
                    Console.Clear();
                }
                while (choice == ConsoleKey.Y);

                if (choice == ConsoleKey.N)
               
                 break;
              
            }
            while (choice != ConsoleKey.Escape);
            Console.WriteLine("\n    Good bye ;)");
            Console.ReadLine();

        }
    }
}