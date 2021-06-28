using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace _123
{
    class Program
    {
        private static Stopwatch tim = new Stopwatch();
        public static bool found = false;
        public static List<char[]> chars5 = new List<char[]>();
        

        static void Main(string[] args)
        {
            Console.WriteLine("Введите хеш");
            string hsh = Console.ReadLine();

            Console.WriteLine("Введите желаемое количество потоков");
            int count = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                chars5.Add(new char[] { 'a', 'a', 'a', 'a', 'a' });
            }

            Task[] tasks5 = new Task[count];
            
            tim.Start();
            for (int i = 0; i < tasks5.Length; i++)
            {
                int z = i;
                tasks5[i] = Task.Run(() => Asydecrypthash5(hsh, count, z, chars5));
            }


            for (int i = 0; i < count; i++)
            {
                tasks5[i].Wait();
            }

            Console.WriteLine(Convert.ToDouble(tim.ElapsedMilliseconds) / 1000 + "s");
            tim.Stop();
            Console.ReadKey();
        }

        public static void Asydecrypthash5(string hsh, int count, int z, List<char[]> chars)
        {
            int[] digits = new int[count];
            int b = 26 / count;
            char[] ch = chars[z];

            for (int i = 0; i < count; i++)
            {

                digits[i] += b * i;

            }
            if (z == 0)
            {
                ch[0] = 'a';
            }
            else
            {
                for (int j = 0; j < b; j++)
                {
                    ch[0]++;
                }
            }

            while (true)
            {
                if (found) break;

                else if (ch[0] == 'z' - digits[z] && ch[1] == 'z' && ch[2] == 'z' && ch[3] == 'z' && ch[4] == 'z') break;

                else if (ComputeHash(ch) == hsh)
                {
                    found = true;
                    Console.WriteLine("Ready!");
                    Console.WriteLine(new string(ch));
                    break;
                }

                else
                {
                    ch[ch.Length - 1]++;
                    if (ch[ch.Length - 1] == '{')
                    {
                        for (int i = 0; i < ch.Length - 1; i++)
                        {
                            if (ch[ch.Length - 1 - i] == '{')
                            {
                                ch[ch.Length - 1 - i] = 'a';
                                ch[ch.Length - 2 - i]++;
                            }
                        }
                    }
                }
            }
        }

        private static SHA256 sha256Hash;
        public static string ComputeHash(char[] input)
        {
            sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            foreach (var h in bytes)
            {
                builder.Append(h.ToString("x2"));
            }
            return builder.ToString();
        }

    }
}