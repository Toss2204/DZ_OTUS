using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Threading.Channels;

namespace DZ_1_Izotov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //сначала задаю переменные
            string NamePol="";
            var InsertCommand = "";
            DateTime DateApp = new DateTime(2024, 11, 18, 20,05,01);
            string VersionApp = "1.0";
            string Help= "Можно ввести одну из указанных команд:\n" +
                        "/start - авторизация\n" +
                        "/help - справка\n" +
                        "/info - информация о программе\n" +
                        "/echo - возврат введенного значения\n" +
                        "/exit - выход";

            char N= 'A';
            
            //теперь запускаю вечный цикл
            while (InsertCommand != "/exit") {

                //укажем запятую перед именем пользователя
                if (NamePol!="" && NamePol.IndexOf(",")<0)
                {
                    NamePol = ", " + NamePol;
                }

                //приветственное сообщение
                Console.WriteLine("Добрый день"+ NamePol + "! Введите одну из указанных команд:");
                Console.WriteLine("/start");
                Console.WriteLine("/help");
                Console.WriteLine("/info");
                Console.WriteLine("/exit");
                Console.WriteLine("");

                InsertCommand =Console.ReadLine();

                //обработка введенного ключа
                switch (InsertCommand)
                {
                     case "/start":
                        if (NamePol == "") 
                        {
                            Console.WriteLine("Введите, пожалуйста, свое имя");
                        }

                        var newNameUser=Console.ReadLine();

                        //на всякий случай от null избавимся
                        if (newNameUser==null)
                        {
                            newNameUser = "";
                        }
                        NamePol = newNameUser.ToString();                        
                        break;

                    case "/help":
                        Console.WriteLine(Help);
                        break;
                    case "/info":
                        Console.WriteLine("Версия программы: "+VersionApp+" Дата релиза: "+ DateApp);
                        break;
                    
                    default:
                        //избавимся от null и вызовем внешний метод, который сообщит остальную строку после /echo
                        if (InsertCommand == null)
                        {
                            InsertCommand = "";
                        }
                        EchoMethod(InsertCommand); 
                        break;
                }

                Console.WriteLine("");
            }

            static  void EchoMethod(string InsCom)
            {
                
                int indexOfSubstring = InsCom.IndexOf("/echo");
                if (indexOfSubstring >= 0)
                {
                    InsCom = InsCom.Replace("/echo", "");
                    InsCom = InsCom.Trim();
                    Console.WriteLine(InsCom);
                }
            }

        }
    }
}
