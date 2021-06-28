using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace ConsoleApp1
{


    class Stack_arr
    {
        public ulong Noperations = 0;                                                                  //количество операций
        private Random rnd = new Random();
        private int size;                                                                              //размер стэка
        private int[] storage;                                                                         //хранилище стэка
        private int head;                                                                              //голова стэка

        public Stack_arr()                                                                             //конструктор стэка
        {

            size = 0;                                                                                  // + 1 
            storage = new int[] { };                                                                   // + 1
            head = -1;                                                                                 // + 1
            //Noperations += 3;                                                                          
        }

        public int Top() { Noperations++; return head; }                                               // + 1; метод, возвращающий текущую голову стэка      

        public int Length() { Noperations++; return size; }                                            // + 1; метод, возвращающий длину стэка               

        public void Push(int elem_new)                                                                 // 13 + 7*size; метод, добавляющий элемент в стэк 
        {
            int[] b = new int[size + 1];                                                               // + 1 + 1 + 1; инициализация массива с новым элементом
            Noperations += 3;
            b[0] = elem_new;                                                                           // + 1 + 1
            Noperations += 2;
            Noperations += 4;
            for (int i = 1, j = 0; i < size + 1; i++, j++) { b[i] = storage[j]; Noperations += 7; }    // + 4 + sum[from 1 to size]( + 1 + 1 + 1 + 1 + 1 + 1 + 1) = 4 + size*7
            storage = b;                                                                               // + 1
            Noperations++;
            head = storage[0];                                                                         // + 1 + 1
            Noperations += 2;
            size++;                                                                                    // + 1
            Noperations++;
        }

        public int Pop()                                                                               // 10 + 6*size;  метод, удаляющий элемент из головы
        {
            int[] b = new int[size - 1];                                                               // + 3;  инициализация массива без удаленного элемента
            Noperations += 3;
            int tmp = storage[0];                                                                      // + 2
            Noperations += 2;
            Noperations += 3;
            for (int i = 1, j = 0; i < size; i++, j++) { b[j] = storage[i]; Noperations += 6; }        // + 3 + sum[from 1 to size-1](+ 6) = 2 + (size - 1)*6 = 6*size - 3; добавляем в массив "b" числа из хранилища с 1 по последнее 
            storage = b;                                                                               // + 1
            Noperations++;
            Noperations++;
            if (b.Length == 0)                                                                         // + 1 + 1 + 1 (это трудоемкость метода Length()) условная конструкция для головы стэка нулевой/ненулевой длины 
            {
                head = -1;                                                                             // + 1 
                Noperations++;
            }
            else                                                                                       // + 2
            {
                head = storage[0];
                Noperations += 2;
            }
            size--;                                                                                    // + 1
            Noperations += 2;
            return tmp;                                                                                // + 1
        }

        public void counting_sort()
        {
            int i, max;                                                                                // + 1
            Noperations++;
            Noperations += 3;
            for (i = 0, max = 0; i < size; i++)                                                        // 3 + sum[from 0 to size-1](2 + 1 + 1 + 13 + 54*index + 23*size*index + 1 + 1 + 13 + 54*index + 23*size*index) = 3 + (size-1)(32 + 108*index + 52*size*index)
            {                                                                                          // 
                Noperations += 2;
                Noperations += 2;
                if (Get(i) > max) { max = Get(i); Noperations += 2; }                                  // находим наибольший элемент из стэка
            }
            Stack_arr cnt = new Stack_arr();                                                           // + 3; временный стэк, необходимый для накопления элементов
            Noperations += 3;
            Stack_arr _out = new Stack_arr();                                                          // + 3; временный стэк, необходимый для вывода 
            Noperations += 3;
            Noperations += 2;
            for (i = 0; i <= max; i++) { cnt.Push(0); Noperations += 3; }                              // + 2 + sum[from 0 to max](+ 2 + 1 + 13 + 7*cnt.size) = 18 + 7*cnt.size + 16*max + 7*cnt.size*max; заполняем стэк нулями (то есть индексы этого стэка (cnt) являются числами на вывод (поэтому его размер равен наибольшему элементу в неотсортированном стэке)), а элементы на этих индексах - это их количество) 
            Noperations += 2;
            for (i = 0; i < size; i++) { cnt.Set(Get(i), cnt.Get(Get(i)) + 1); Noperations += 7; }     // + 2 + sum[from 0 to size - 1](+ 7 + 10 + 54*cnt.index + 13*cnt.size + 26*cnt.size*cnt.index + 13 + 54*index + 26*size*index + 13 + 54*cnt.index + 23*cnt.size*cnt.index + 13 + 54*index + 23*size*index) = 2 + (56*index + 108*cnt.index + 13*cnt.size + 52*cnt.size*cnt.index + 52*size*index)*(size-1); накопление значений по индексам
            Noperations += 2;
            for (i = 0; i <= max; i++)                                                                 // + 2 +  sum[from 0 to max](2 + 13 + 54*cnt.index + 26*cnt.size*cnt.index  sum[from 0 to 5](+ 2 + 3 + 1 + 13 + 54*cnt.index + 26*cnt.size*cnt.index + 10 + 54*cnt.index + 13*cnt.size + 26*cnt.size*cnt.index + 13 + 54*cnt.index + 26*cnt.size*cnt.index + 13 + 7*_out.size)) = 277 + 810*cnt.index + 390*cnt.size*cnt.index + 65*cnt.size + 35*_out.size + 15*max+54*max*cnt.index+ 26*max*cnt.size*cnt.index; выписываем значения во временный стэк _out, пока значение на индексе из стэка cnt не будет равно 0
            {                                                                                          //
                Noperations += 2;
                while (cnt.Get(i) > 0)                                                                 // пока значение на данном индексе не будет равно нулю, индекс будет добавляться в стэк _out
                {                                                                                      //
                    Noperations += 2;
                    cnt.Set(i, cnt.Get(i) - 1);                                                        // уменьшаем значение
                    Noperations += 3;
                    _out.Push(i);                                                                      // добавляем в стэк _out
                    Noperations++;
                }                                                                                      //
            }                                                                                          //
            Noperations += 2;
            for (i = 0; i < size; i++) { Set(_out.Length() - i - 1, _out.Get(i)); Noperations += 7; }   // + 2 + sum[from 0 to size - 1](2 + 10 + 54*index + 13*size + 26*size*index ) = + 2 + (size-1)(13 + 54*index + 13*size + 26*size*index ); добавляем заменяем элементы из рабочего стэк элементами из стэка(_out)
        }

        public int Get(int index)                                                                      // 13 + 54*index + 26*size*index; 
        {
            int needed;                                                                                // + 1
            Noperations++;
            Stack_arr tmp = new Stack_arr();                                                           // + 3   
            Noperations += 3;
            Noperations += 2;
            for (int i = 0; i < index; i++) { tmp.Push(Pop()); Noperations += 4; }                     // + 2 + sum[from 0 to index - 1](4 + 13 + 7*size + 10 + 6*size) = 2 + 27*index + 13*size*index; удаляем элементы до нужного индекса, записывая удаленные элементы во временный стэк(tmp)                                                                          
            needed = Top();                                                                            // + 1 + 1 + 1; нужный элемент становится головой полученного стэка
            Noperations += 2;
            Noperations += 3;
            for (int i = index - 1; i >= 0; i--) { Push(tmp.Pop()); Noperations += 4; }                // + 3 + sum[from 0 to index](4 + 13 + 7*size + 10 + 6*size) = 3 + 27*index + 13*size*index; возвращаем удаленные элементы в стэк, с которым работаем                                                                         
            Noperations++;
            return needed;                                                                             // + 1
        }

        public void Set(int index, int change)                                                         // 10 + 54*index + 13*size + 26*size*index
        {
            Stack_arr tmp = new Stack_arr();                                                           // + 3
            Noperations += 3;
            Noperations += 2;
            for (int i = 0; i < index; i++) { tmp.Push(Pop()); Noperations += 2; }                     // 2 + 27*index + 13*size*index; удаляем элементы до нужного индекса, записывая удаленные элементы во временный стэк(tmp)
            Noperations += 2;
            Pop();                                                                                     // 1 + 10 + 6*size; удаляем элемент, который хотим изменить (не записываем его в tmp)  
            Noperations++;
            Push(change);                                                                              // 1 + 13 + 7*size 
            Noperations++;
            Noperations += 3;
            for (int i = index - 1; i >= 0; i--) { Push(tmp.Pop()); Noperations += 2; }                // 3 + 27*index + 13*size*index
            Noperations += 3;
        }

        public void info()
        {
            Stack_arr tmp1 = new Stack_arr();
            for (int i = 0; i < size;)
            {
                tmp1.Push(Pop());
                Console.Write(tmp1.Top() + " ");
            }
            for (int i = 0; i < tmp1.Length();) Push(tmp1.Pop());
            Console.WriteLine("");
            Console.WriteLine("Всего элементов " + size + "\n");
        }
    }

    class Program
    {
        private static Random rnd = new Random();
        private static Stopwatch tim = new Stopwatch();
        private static ulong b = 0;

        static void Main(string[] args)
        {
            (int, int)[] a = new (int, int)[10];

            //List<Stack_arr> a = new List<Stack_arr>();
            //for (int j = 0; j < 10; j++) a.Add(new Stack_arr());
            //int i = 1;
            //for (int k = 0; k < a.Count;)
            //{
            //    tim.Start();
            //    for (int j = 0; j < 100 * i; j++) a[0].Push(rnd.Next(1, 1000));
            //    a[0].counting_sort();
            //    Console.WriteLine("Size: " + a[0].Length());
            //    b = (ulong)(a[0].Length());
            //    Console.Write("Theoretical");
            //    GetTheoreticalValue();
            //    Console.WriteLine("Operations  : " + a[0].Noperations);
            //    tim.Stop();
            //    Console.WriteLine("Time: " + Convert.ToDouble(tim.ElapsedMilliseconds) / 1000 + "s" + "\n");
            //    i++;
            //    a.RemoveAt(0);
            //    tim.Reset();
            //}
        }

        public static void GetTheoreticalValue()
        {
            ulong size, index, cntsize, cntindex, _outsize, max;

            size = b;
            index = size;
            cntsize = index;
            cntindex = cntsize;
            _outsize = cntindex;
            max = size;
            ulong a = (ulong)(3 + (size - 1) * (45 + 218 * index + 13 * cntsize + 52 * cntsize * cntindex + 130 * size * index + 13 * size + 108 * cntindex) + 299 + 72 * cntsize + 31 * max + 7 * cntsize * max + 810 * cntindex + 390 * cntsize * cntindex + 35 * _outsize + 54 * max * cntindex + 26 * max * cntsize * cntindex);
                Console.WriteLine(" : " + a); //asdasdasdasdasd
        }
    }
}


















