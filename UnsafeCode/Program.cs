using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace UnsafeCode
{
    class Program
    {
        unsafe static void Increment(int* p)
        {
            *p = *p + 1;
        }

        unsafe static void Main(string[] args)
        {
            char* p = (char*)Marshal.AllocHGlobal(20*sizeof(char));
            string str = "Hello, HuZhiqian";
            char* currentp = p;
            for (int i = 0; i < str.Length; i++)
            {
                *currentp++ = str[i];
            }
            string newStr = new string(p, 0, (int)(currentp - p));
            char* p2 = p;
            while (*p2!='\0')
            {
                Console.Write(*p2);
                p2++;
            }
            Console.WriteLine("|||");
            Marshal.FreeHGlobal((IntPtr)p);
            Console.WriteLine(newStr);
            Console.Read();
        }
    }
}
