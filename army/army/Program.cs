using System;
using System.Collections.Generic;
namespace army
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<int> gavno = new List<int>();
            List<int> gavno2 = new List<int>();
            List < List<int> > suka = new List<List<int>>();
            for (int i = 0; i < 100; i++)
            {
                gavno.Add(i+1);
                gavno2.Add(i+1);
            }
            Console.WriteLine("dsfsdfsdf");

            for (int i = 0; i < 100; i++)
            {
                if ((gavno[i] % gavno2[i]) != 0 || (gavno2[i] % gavno[i]) != 0 )
                {

                }
            }
















            //for(int i = 0; i< 100; i++)
            //{
            //    for (int j = 1; j <100; j++)
            //    {
            //        if (gavno[i] > gavno[j])
            //        {
            //            if (gavno[i] % gavno[j] != 0)
            //            {
            //                Console.Write($" {gavno[i]}");
            //            }

            //            else
            //            {
            //                break;
            //            }
            //        }

            //        else 
            //        {
            //            if (gavno[j] % gavno[i] != 0)
            //            {
            //                Console.Write($" {gavno[j]}");
            //            }

            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
        }
    }
}
