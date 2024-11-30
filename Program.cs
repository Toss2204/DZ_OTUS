using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Threading.Channels;
using System.Xml.Linq;

namespace DZ_1_Izotov
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //сначала задаю переменные
            string namePol = "";
            var insertCommand = "";
            DateTime dateApp = new DateTime(2024, 11, 18, 20, 05, 01);
            string versionApp = "1.0";


            List<string> tasks = new List<string>();

            //теперь запускаю вечный цикл
            while (insertCommand != "/exit")
            {

                //укажем запятую перед именем пользователя
                if (namePol != "" && namePol.IndexOf(',') < 0)
                {
                    namePol = ", " + namePol;
                }
                string message = $"Добрый день{namePol}! Введите одну из указанных команд:";
                //приветственное сообщение
                Console.WriteLine(message);
                Console.WriteLine("/start");
                Console.WriteLine("/help");
                Console.WriteLine("/info");
                Console.WriteLine("/adtask");
                Console.WriteLine("/showtasks");
                Console.WriteLine("/removetask");
                Console.WriteLine("/exit");
                Console.WriteLine("");

                insertCommand = Console.ReadLine();

                bool indexOfSubstring = insertCommand.Contains("/echo");
                if (indexOfSubstring == true)
                {
                    //избавимся от null и вызовем внешний метод, который сообщит остальную строку после /echo
                    if (insertCommand == null)
                    {
                        insertCommand = "";
                    }
                    EchoMethod(insertCommand);

                }
                else
                {
                    //обработка введенного ключа
                    switch (insertCommand)
                    {
                        case "/start":

                            namePol = StartMethod(namePol);
                            break;

                        case "/help":
                            HelpMethod();
                            break;
                        case "/info":
                            InfoMethod(versionApp, dateApp);
                            break;
                        case "/adtask":
                            AdTaskMethod(ref tasks);
                            break;
                        case "/showtasks":
                            ShowTasksMethod(ref tasks);
                            break;
                        case "/removetask":
                            bool Res = RemoveTaskMethod(ref tasks);
                            while (Res != true)
                            {
                                Res = RemoveTaskMethod(ref tasks);
                            }
                            break;
                        default:
                            if (insertCommand == "/exit")
                            {
                                Console.WriteLine($"До свидания{namePol}");
                            }
                            else
                            {
                                Console.WriteLine("Введены неточные данные. Повторите попытку");
                            }
                            break;

                    }
                }

                Console.WriteLine("");
            }




        }

        static void EchoMethod(string InsCom)
        {

            InsCom = InsCom.Replace("/echo", "");
            InsCom = InsCom.Trim();
            Console.WriteLine(InsCom);

        }

        static string StartMethod(string namePol)
        {

            if (namePol == "")
            {
                Console.WriteLine("Введите, пожалуйста, свое имя");
            }

            var newNameUser = Console.ReadLine();

            //на всякий случай от null избавимся
            if (newNameUser == null)
            {
                newNameUser = "";
            }
            return newNameUser.ToString();

        }

        static void HelpMethod()
        {
            string help = "Можно ввести одну из указанных команд:\n" +
                    "/start - авторизация\n" +
                    "/help - справка\n" +
                    "/info - информация о программе\n" +
                    "/echo - возврат введенного значения\n" +
                    "/adtask - добавить задачу в список задач\n" +
                    "/showtasks - показать список задач\n" +
                    "/removetask - удалить указанную задачу\n" +
                    "/exit - выход";
            Console.WriteLine(help);

        }

        static void InfoMethod(string versionApp, DateTime dateApp)
        {
            Console.WriteLine($"Версия программы: {versionApp} Дата релиза: {dateApp}");
            DateTime dateUpdApp = new DateTime(2024, 11, 30, 20, 05, 02);
            Console.WriteLine($"Обновлено: {dateUpdApp}");
        }

        static void AdTaskMethod(ref List<string> tasks)
        {
            Console.WriteLine("Введите, пожалуйста, описание задачи");
            var newTask = Console.ReadLine();

            //на всякий случай от null избавимся
            if (newTask == null)
            {
                newTask = "";
            }

            if (newTask == "")
            {
                Console.WriteLine("Пустую задачу ввести нельзя. Пожалуйста, повторите ввод");
                newTask = Console.ReadLine();
            }

            tasks.Add(newTask);

        }


        static void ShowTasksMethod(ref List<string> tasks, string message = "Список задач:")
        {
            if (tasks.Count > 0)
            {
                Console.WriteLine(message);
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
            else
            {
                Console.WriteLine("Список задач пуст");
            }

        }

        static bool RemoveTaskMethod(ref List<string> tasks)
        {

            if (tasks.Count > 0)
            {

                string mes = "Укажите номер задачи для удаления из списка:";
                ShowTasksMethod(ref tasks, mes);

                var numberOfTask = Console.ReadLine();

                //на всякий случай от null избавимся
                if (numberOfTask != null)
                {

                    bool result = int.TryParse(numberOfTask, out var number);
                    Console.WriteLine();
                    {
                        if (result == true)
                        {
                            if (number <= tasks.Count && number > 0)
                            {
                                string currentTask = tasks[number - 1];
                                tasks.RemoveAt(number - 1);
                                Console.WriteLine($"Задача {number}.{currentTask} удалена из списка");
                                Console.WriteLine();
                                ShowTasksMethod(ref tasks);
                                return true;
                            }
                            else
                            {
                                Console.WriteLine($"ВНИМАНИЕ! Вы вышли за границы списка. Максимальное число для ввода:{tasks.Count}, а минимальное: 1");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ВНИМАНИЕ! Вводить можно только число");
                        }
                    }


                }
                else
                {
                    Console.WriteLine("ВНИМАНИЕ! Вводить можно только число");
                }

            }
            else
            {
                Console.WriteLine("ВНИМАНИЕ! Список задач пуст");
                return true;
            }

            return false;

        }
    }
}
