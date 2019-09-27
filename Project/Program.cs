using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    
    class Program
    {
        
       static dynamic buf;
        static void Login()
        {

            Console.WriteLine("Введите логин");
        }
        static string Enter_Password(MethodBase method)
        {
           // Console.WriteLine("Введите пароль");
            string password="";
            var collection = Return_Desir();
            if (method.Name== "Registration")
            {
                
                if (password.Length>=8&&password.Length<=16)
                {
                    bool num=false;
                    bool letter=false;
                    for (int i = 0; i < password.Length; i++)
                    {
                        if (password[i]>'0'&&password[i]<'9'&& !num)
                        {
                            num = true; 
                        }
                        if (password[i]>='a'&&password[i]<='z'&&!letter)
                        {
                            letter = true;
                        }
                        if (num&&letter)
                        {
                            if (collection.Password_Exist(password))
                            {
                                return Enter_Password(method);
                            }
                            else
                            {
                                return password;
                               
                            }
                            
                        }
                        else if(i==password.Length-1)
                        {
                            if (!num)
                            {
                                Console.WriteLine("Не введено ни одной цифры");
                            }
                            if (!letter)
                            {
                                Console.WriteLine("Не введено ни одной буквы");
                            }
                            if (!(num||letter))
                            {
                                Console.WriteLine("Введены некоректные символы");
                            }
                           return Enter_Password(method);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Длина пароля некоректна ,введите от 8 до 16 символов ");
                    Enter_Password(method);
                }

                if (collection.Password_Exist(password))
                {
                    return Enter_Password(method);
                }
                else
                {
                    return password;
                }

            }

            return "";

            
            
        }
        static void Enter_type()
        {
           
            Console.WriteLine("Введите режим ");
            Console.WriteLine("Пользователь -1 , Менеджер -2 , Инженер -3");
            Console.WriteLine("Ввод");
            char enter;
           
            

            
            enter = Convert.ToChar(Console.ReadLine());
            switch (enter)
            {
                case '1':
                    Console.WriteLine(enter);
                    buf = new Student();
                   

                    break;

                case '2':
                    Console.WriteLine(enter);
                    Manager man = new Manager();
                   buf = man;
                    
                    break;

                case '3':
                    Console.WriteLine(enter);
                    Engineer eng = new Engineer();
                    buf = eng;
                    break;

                default:
                    Console.WriteLine("error");
                    
                    Enter_type();
                    break;
            }
           
        }
        static Collection Return_Desir()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("collection.dat", FileMode.OpenOrCreate);
             Collection collection = (Collection)formatter.Deserialize(stream);
            return collection;
        }

        
        static string Enter_name()
        {
            string n;
            Console.WriteLine("Введите ваши данные");
            n = Console.ReadLine();
           


            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] >= '0' && n[i] <= '9')
                {
                    Console.WriteLine("Имя содержит цифру");
                    
                    return Enter_name();
                   
                }
            }
                    
                    if (n.Length <= 1)
                    {
                Console.WriteLine("Не введенны данные");
                        return Enter_name();
                        
                    }
           
            
                    for (int i = 0; i < n.Length; i++)
                    {
                         if (i==0)
                         {
                    
                          }
                else
                {
                    n.ToLower();
                }
                    }
            return n;

                  
                    
                
            }
            
        

        static void Color()
        {
            Console.Title = "Очередь";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Вас приветсвует система регистрации");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Вход  ");


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("            Регестрация");
            Console.ResetColor();

            
        }
        static void First_Enter()
        {
            char enter ;
            Console.WriteLine("Нажмите 1 для входа и 2 для регестрации");
            enter = Convert.ToChar(Console.ReadLine());
            switch (enter)
            {
                case '1':
                    Enterentrance();
                    break;

                case '2':
                    Registration();
                    break;

                default:

                    Console.WriteLine("error");
                    Console.WriteLine(enter);
                    First_Enter();
                    break;
            }
        }

        static void Enterentrance()
        {
            Login();
            //Password();
            
            // 

            
        }

        static void Registration()
        {
            string login, password, name, surname, otch, passport, sex,type;
            MethodBase method = MethodInfo.GetCurrentMethod();

            login = Enter_name();
                        
            password = Enter_Password(method);
            name = Enter_name();




            Console.WriteLine(method.Name);

         

            Console.Write("Логин  \n");
            

           
        }
        static void Main(string[] args)
        {

           

            try
            {
                 Collection collection = new Collection();

                BinaryFormatter formatter = new BinaryFormatter();

                FileStream stream = new FileStream("collection.dat", FileMode.Append);
                formatter.Serialize(stream, collection);
                stream.Close();
               //  Color();
               //  First_Enter();*/

                //  string n = Enter_name();
                // Console.WriteLine(n);
                Color();
                Registration();
               
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
           
        finally { Console.ReadKey(); }
            
        }
    }
}
