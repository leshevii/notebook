using System;
using System.IO;

namespace Lesson7
{
    class MainClass
    {
        public static Record fillRecord()
        {
            string title, desc, author;
            bool status;
            Console.WriteLine("Заполните свой ежедневник");
            Console.WriteLine("Введите заголовок темы: ");
            title = Console.ReadLine();
            Console.WriteLine("Введите описание: ");
            desc = Console.ReadLine();
            Console.WriteLine("Введите имя автора темы:");
            author = Console.ReadLine();

            Console.WriteLine("Укажите статус да/нет:");
            if (Console.ReadLine().ToLower() == "да")
            {
                status = true;
            }
            else
            {
                status = false;
            }
            Record rec = new Record(title, desc, author, status);
            return rec;
        }
        public static Record[] fill()
        {
            Record[] records = new Record[0];
            int index = 0;            
            while (true)
            {
                Record rec = fillRecord();
                Array.Resize<Record>(ref records, records.Length + 1);
                records[index++] = rec;

                Console.WriteLine("Завершить ввод да/нет");
                if (Console.ReadLine().ToLower() == "да") break;
            }
            return records;
        }
        public static void delete(ref Notebook not,string path)
        {
            Console.WriteLine("Удалить по номеру да/нет");

            if (Console.ReadLine().ToLower() == "да")
            {
                Console.WriteLine("Введите номер от 0 - " + (not.Records.Length-1));
                not.Delete(Convert.ToInt32(Console.ReadLine()));
                not.Save(path, false);
            }
            else
            {

            }
        }
        public static void reorder(ref Notebook not)
        {
            Console.WriteLine("Упорядочить записи по:");
            Console.WriteLine("Заголовку(title) - 0");
            Console.WriteLine("Автору(author)   - 1");
            Console.WriteLine("Статусу(status)  - 2");

            int order;
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 0:
                    Console.WriteLine("По возрастанию/убыванию: 1/0");
                    order = Convert.ToInt32(Console.ReadLine());
                    if(order==1)
                       not.Reorder(Notebook.Field.title, true);
                    else
                        not.Reorder(Notebook.Field.title, false);
                    not.Save(Directory.GetCurrentDirectory() + "/ivan.txt", false);
                    break;
                case 1:
                    Console.WriteLine("По возрастанию/убыванию: 1/0");
                    order = Convert.ToInt32(Console.ReadLine());
                    if (order == 1)
                        not.Reorder(Notebook.Field.author, true);
                    else
                        not.Reorder(Notebook.Field.author, false);
                    not.Save(Directory.GetCurrentDirectory() + "/ivan.txt", false);
                    break;
                case 2:
                    Console.WriteLine("Решен/не решен: 1/0");
                    order = Convert.ToInt32(Console.ReadLine());
                    if (order == 1)
                        not.Reorder(Notebook.Field.status, false);
                    else
                        not.Reorder(Notebook.Field.status, true);
                    not.Save(Directory.GetCurrentDirectory() + "/ivan.txt", false);
                    break;
            }
        }
        public static void edit(ref Notebook not)
        {
            int num;
            string text;            

            not.Print();
            while (true)
            {
                

                Console.WriteLine("Введите номер записи - 0-" + (not.Records.Length - 1));
                num = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Укажите номер поля для редактирования");
                Console.WriteLine("0-Заголовк(title)");
                Console.WriteLine("1-Описание(desc)");
                Console.WriteLine("2-Автор(author)");
                Console.WriteLine("3-Статус(status)");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 0:
                        Console.WriteLine("Введите title - ");
                        text = Console.ReadLine();
                        not.Edit(num,text);
                        break;
                    case 1:
                        Console.WriteLine("Введите desc - ");
                        text = Console.ReadLine();
                        not.Edit(num, null,text);
                        break;
                    case 2:
                        Console.WriteLine("Введите author - ");
                        text = Console.ReadLine();
                        not.Edit(num,null,null,text);
                        break;
                    case 3:
                        Console.WriteLine("Введите status да/нет - ");
                        if (Console.ReadLine().ToLower() == "да")
                        {
                            not.Edit(num, null, null, null, true);                            
                        }
                        break;
                }
                Console.WriteLine("Продолжить ввод да/нет");
                if (Console.ReadLine().ToLower() == "нет") break;
            }
        }
        public static void Main(string[] args)
        {
            Record[] records;
            Notebook notebook;

            string def = Directory.GetCurrentDirectory() + "/default.txt";

            if (!File.Exists(def))
            {
                records = fill();
                notebook = new Notebook(records);
                notebook.Save(def);
            }
            else
            {
                notebook = new Notebook(1);
                Console.WriteLine("Загрузить по диапазону дат ? да/нет");
                if(Console.ReadLine().ToLower() == "да")
                {                    
                    Console.WriteLine("Введите начальную дату в формате xx.xx.xxxx");
                    string[] strFrom = Console.ReadLine().Split('.');
                    Console.WriteLine("Введите конечную дату в формате xx.xx.xxxx");
                    string[] strTo = Console.ReadLine().Split('.');

                    DateTime from = new DateTime(Convert.ToInt32(strFrom[2]), Convert.ToInt32(strFrom[1]), Convert.ToInt32(strFrom[0]));
                    DateTime to = new DateTime(Convert.ToInt32(strTo[2]), Convert.ToInt32(strTo[1]), Convert.ToInt32(strTo[0]));

                    notebook.Load(def, from, to);                    
                }
                else
                {
                    notebook.Load(def);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Ваш ежедневник инициализирован: ");
            Console.WriteLine();
            int num;
            while (true)
            {                
                Console.WriteLine("-------Меню-------");
                Console.WriteLine("Вывести записи - 0");
                Console.WriteLine("Отсортировать  - 1");
                Console.WriteLine("Редактировать  - 2");
                Console.WriteLine("Создать        - 3");
                Console.WriteLine("Удалить        - 4");
                Console.WriteLine("Выход          - 5");
                Console.WriteLine("------------------");
                num = Convert.ToInt32(Console.ReadLine());
                if (num == 5) break;

                switch (num)
                {
                    case 0:
                        notebook.Print();
                        break;
                    case 1:
                        reorder(ref notebook);
                        break;
                    case 2:
                        edit(ref notebook);
                        break;
                    case 3:
                        notebook.AddRecord(fillRecord());
                        notebook.Save(def);
                        break;
                    case 4:
                        delete(ref notebook,def);
                        break;                    
                }
            }
        }
    }
}
