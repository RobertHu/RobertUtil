using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agorithim
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 5,4,8,1,3,6,12};
            InsertSort.Sort(input);
            foreach (var item in input)
            {
                Console.Write(string.Format("{0}  ",item));
            }
            Console.Read();
        }
    }
}
