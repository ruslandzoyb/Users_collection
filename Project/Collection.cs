using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace Project
    
{
    [Serializable]
   public class Collection:IEnumerable,IEnumerator
    {
        private User[] list;                  // Массив элементов граждан
        private User[] list_2;               // Массив граждан для перебора 
        private User buffer;
        private static ushort num_of_coll;
        public int num_hash;
        private int indexer = -1;
        private bool exist;

        private int last_pos;                 // Последняя поизиция входа элемента 
        private int am = 0;                     // Счетчик входов

        private bool loop;
        private byte loop_num;
        public Collection()
        {
            num_of_coll++;
            num_hash = num_of_coll;
        }

        public void Add(User user)        // Метод добавления граждан в колекцию
        {
            if (!Equals(user))
            {


                if (list == null)
                {

                    list = new User[1];
                    list[0] = user;


                }
                else if (user is Pensioner)
                {                                                           // Проверка ,является элемент типа Пенсионер
                    if (this.Contains<Pensioner>())                     // Проверка на содержание массива элементами типа
                                                                        //пенсионер 
                    {
                        list_2 = new User[list.Length + 1];        // Создание массива на 1 элемент больше чем 1
                        for (int i = 0; i <= last_pos; i++)           // Запись всех пенсионеров в 2ой массив
                        {
                            list_2[i] = list[i];
                        }
                        list_2[last_pos + 1] = user;              // Запись последнего добаленого пенсионера

                        for (int i = last_pos + 2; i < list_2.Length; i++)  // Запись всех остальных элементов в массив
                        {
                            list_2[i] = list[i - 1];
                        }
                        list = new User[list_2.Length];
                        list = list_2;
                        list_2 = null;
                    }
                    else
                    {                                              // Запись 1ого пенсионера в коллекцию
                        list_2 = new User[list.Length + 1];     // Создание массива на 1 элемент больше чем 1
                        list_2[0] = user;                        // запись пенсионера на 1ую позицию в массиве
                        for (int i = 1; i < list_2.Length; i++)   // запись остальных членов в массив
                        {
                            list_2[i] = list[i - 1];
                        }
                        list = new User[list_2.Length];
                        list = list_2;
                        list_2 = null;
                    }
                    //  
                }          // Проверка ,является элемент типа Пенсионер
                else
                {
                    list_2 = new User[list.Length + 1];  // Инициализация массива 2 на 1 эл больше чем 1 массив 
                    list_2[list_2.Length - 1] = user;      // Добавление последнего эл в конец 2 массива
                    for (int i = 0; i <= list.Length - 1; i++)  // Добаление остальных элементов в 2 массив 
                    {
                        list_2[i] = list[i];
                    }
                    list = new User[list_2.Length];        // Переприсваеваем 1 массив ,увеличивая размер на 1
                    list = list_2;                            // Переносим элементы из 2 массива в 1-ый
                    list_2 = null;                             // Обнуляем 2 массив
                }
            }
            else if (Equals(user))
            {
                Console.WriteLine("{0} {1} ", user.Name, user.Passport);
                Console.Write("уже есть в списке под номером \n {0}  ", loop_num);

                ShowPersonal(list[loop_num]);
            }

        }
        public void AddRange(User[] Users)
        {

            for (int i = 0; i < Users.Length; i++)
            {
                this.Add(Users[i]);
            }
        }
        public void AddCollection(Collection collection)
        {
            this.AddRange(collection.list);
        }

        public bool Contains<T>() where T : User   // Обобщенный метод для проверки вхождения определенного типа и количсество его вхождений
        {
            last_pos = 0;
            am = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] is T)
                {
                    last_pos = i;
                    ++am;

                }

            }
            if (am == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User this[int index]
        {
            get
            {
                if (index >= 0 && index < list.Length)
                {
                    return list[index];
                }
                else
                {
                    return null;

                }

            }
        }
        public void Show()
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine("{0}  {1}  {2}  ", i, list[i].Name, list[i].GetType());
            }
        }
        public void ShowPersonal(Object persona)
        {
            Console.WriteLine("{0}  {1}  ", ((User)persona).Name, ((User)persona).GetType());
        }

        public void RemoveAt(ushort index)
        {
            if (list.Length == 0)
            {
                Console.WriteLine("Коллекция пустая");
            }
            else if (index > list.Length - 1 || index < 0)
            {
                Console.WriteLine("Некореткное число ");
            }
            else
            {
                list_2 = new User[list.Length - 1];

                if (index == 0)
                {
                    for (int i = 0; i < list_2.Length; i++)
                    {
                        list_2[i] = list[i + 1];

                    }
                }
                else if (index == list.Length - 1)
                {
                    for (int i = 0; i < list_2.Length; i++)
                    {
                        list_2[i] = list[i];
                    }
                }
                else
                {
                    for (int i = 0; i < index; i++)
                    {
                        list_2[i] = list[i];
                    }
                    for (int i = index, j = i; i < list_2.Length; i++)
                    {
                        list_2[i] = list[++j];
                    }

                }
                list = new User[list_2.Length];
                list = list_2;


            }
        }

        public void RemoveAll()
        {
            this.list = null;
            Console.WriteLine("Коллекция удалена");
        }
        public void RemoveAfter(ushort index)
        {

            list_2 = new User[++index];
            --index;
            if (index == 0)
            {
                this.RemoveAll();
            }
            else if (index >= list.Length - 1)
            {
                Console.WriteLine("Конец коллекции ");
            }
            else
            {
                for (int i = 0; i < list_2.Length; i++)
                {
                    list_2[i] = list[i];
                }
                list = new User[list_2.Length];
                list = list_2;
                list_2 = null;
            }
        }
        public void RemoveBefore(ushort index)
        {
            list_2 = new User[list.Length - index];

            if (index == 0)
            {
                Console.WriteLine("Нулевой индекс");
            }
            else if (index >= list.Length)
            {
                Console.WriteLine("Индекс больше размера массива");
            }
            else
            {
                for (int i = 0; i < list_2.Length; i++)
                {
                    list_2[i] = list[i + index];
                }
                list = new User[list_2.Length];
                list = list_2;
                list_2 = null;
            }
        }
        public void RemoveUser(string passport)
        {

            for (int i = 0; i < list.Length; i++)
            {

                if (passport == list[i].Passport)
                {
                    this.RemoveAt((ushort)i);
                }
            }
        }

        public void ChangeEl(ushort index, User User)
        {
            if (index >= list.Length)
            {
                Console.WriteLine("Выход за граници");
            }
            else
            {
                this.list[index] = User;
            }


        }
        public void ChangeData(ushort index)
        {
            Console.WriteLine("Введите имя");
            list[index].Name = Console.ReadLine();
        }
        public User Return_last()
        {
            Console.WriteLine("Последний номер в очереди {0}", list.Length - 1);
            return list[list.Length - 1];
        }

        public bool Password_Exist(string password)
        {
          
            for (int i = 0; i < list.Length; i++)
            {
                if (password==list[i].Passport)
                {
                    exist = true;
                    break;
                }
                else
                {
                    exist = false;
                }
            }
            return exist;
        }



        public override bool Equals(object obj)
        {
            buffer = (User)obj;
            if (list == null)
            {
                loop = false;
            }
            else
            {
                for (int i = 0; i < list.Length; i++)
                {
                    if (buffer.Passport == list[i].Passport)
                    {
                        loop = true;
                        loop_num = (byte)i;
                        break;
                    }
                    else
                    {
                        loop = false;
                    }
                }
            }
            return loop;






        }

        public override int GetHashCode()
        {
            return num_hash;

        }
        public override string ToString()
        {
             this.Show();
            return "";
        }
        public IEnumerator GetEnumerator()
        {
            return this;
        } // Реализация INumerable

        public bool MoveNext()
        {
            if (indexer == list.Length - 1)
            {
                Reset();
                return false;
            }
            else
            {
                ++indexer;
                return true;
            }
        }
        public void Reset()
        {
            indexer = -1;
        }
        public object Current
        {
            get { return list[indexer]; }
        }
        //Реализация Inumerator


        public int Last_pos
        {
            get
            {
                return last_pos;
            }
        }
        public int Am
        {
            get { return am; }
        }
    }
}
