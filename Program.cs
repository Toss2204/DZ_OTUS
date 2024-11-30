using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Threading.Channels;
using System.Xml.Linq;

namespace DZ_1_Izotov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //сначала задаю переменные
            string namePol = "";
            var insertCommand = "";
            DateTime dateApp = new DateTime(2024, 11, 18, 20,05,01);
            string versionApp = "1.0";
            string help= "Можно ввести одну из указанных команд:\n" +
                        "/start - авторизация\n" +
                        "/help - справка\n" +
                        "/info - информация о программе\n" +
                        "/echo - возврат введенного значения\n" +
                        "/exit - выход";

            
            //теперь запускаю вечный цикл
            while (insertCommand != "/exit") {

                //укажем запятую перед именем пользователя
                if (namePol!="" && namePol.IndexOf(',')<0)
                {
                    namePol = ", " + namePol;
                }
                string message = $"Добрый день{namePol}! Введите одну из указанных команд:";
                //приветственное сообщение
                Console.WriteLine(message);
                Console.WriteLine("/start");
                Console.WriteLine("/help");
                Console.WriteLine("/info");
                Console.WriteLine("/exit");
                Console.WriteLine("");

                insertCommand =Console.ReadLine();

                int indexOfSubstring = insertCommand.IndexOf("/echo");
                if (indexOfSubstring >= 0)
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
                            namePol = newNameUser.ToString();
                            break;

                        case "/help":
                            Console.WriteLine(help);
                            break;
                        case "/info":
                            Console.WriteLine("Версия программы: " + versionApp + " Дата релиза: " + dateApp);
                            break;
                        default:
                            Console.WriteLine("Введены неточные данные. Повторите попытку");
                            break;

                    }
                }

                Console.WriteLine("");
            }

            static  void EchoMethod(string InsCom)
            {

                    InsCom = InsCom.Replace("/echo", "");
                    InsCom = InsCom.Trim();
                    Console.WriteLine(InsCom);
                
            }

        }
    }
}
