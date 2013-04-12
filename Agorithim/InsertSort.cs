using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agorithim
{
    public class InsertSort
    {
        public static int[] Sort(int[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                int j = i - 1;
                int key = input[i];
                while (j >= 0 && input[j] > key)
                {
                    input[j + 1] = input[j];
                    j--;
                }
                input[j + 1] = key;
            }
            return input;
        }
    }
}
