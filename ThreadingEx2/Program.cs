using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThreadingEx2;

namespace FindSmallest
{
    class Program
    {

        private static int[][] Data = new int[][]{

            
            new[]{1,5,4,2}, 
            new[]{3,2,4,11,4},
            new[]{33,2,3,-1, 10},
            new[]{3,2,8,9,-1},
            new[]{1, 22,1,9,-3, 5}
        };

        private static int FindSmallest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int smallestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    smallestSoFar = number;
                }
            }
            return smallestSoFar;
        }

        private static int FindBiggest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int biggestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number > biggestSoFar)
                {
                    biggestSoFar = number;
                }
            }
            return biggestSoFar;
        }


        static void Main()
        {

            Generator gen = new Generator();
            gen.generate(500, 500);
            Data = gen.result;

            List<Task<int>> myList = new List<Task<int>>();
          
            foreach (int[] data in Data)
            {

                Task<int> rTask = Task.Run(() =>
                {
                        
                    int smallest = FindSmallest(data);

                    Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
                    
                   
                    return smallest;
                });

                myList.Add(rTask);

               

                
            }

            Task.WaitAll(myList.ToArray());



            //foreach (Task<int> t in myList)
            //{
            //    Console.Write(t.Result.ToString() + " ");
                
            //}

            List<int> rlist = new List<int>();

            foreach (Task<int> t in myList)
            {
                rlist.Add(t.Result);
                
            }

            int smallestofall = FindSmallest(rlist.ToArray());
           
            Console.WriteLine("\t" + String.Join(", ", rlist) + "\n-> " + smallestofall);

            

            
            Console.ReadKey();
           
        }
    }
}
