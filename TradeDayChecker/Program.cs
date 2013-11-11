using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TradeDayChecker
{

    enum Kinds
    {
        One=1,
        Two=1,
        Three,
        Four
    }


    class Program
    {
        private static DateTime _Target = DateTime.Parse("1899-12-30 04:00:00.000");
        static void Main(string[] args)
        {
            //Thread thread = new Thread(Handle);
            //thread.IsBackground = true;
            //thread.Start();
            //Console.Read();
            Kinds myKind = Kinds.Two;
            switch (myKind)
            {
                case Kinds.One:
                    Console.WriteLine("one");
                    break;
                case Kinds.Two:
                    Console.WriteLine("Two");
                    break;
                case Kinds.Three:
                    Console.WriteLine("Three");
                    break;
                case Kinds.Four:
                    Console.WriteLine("Four");
                    break;
            }
           

        }

        private static void Handle()
        {
            DateTime dt = DateTime.Now;
            if (dt.Hour == _Target.Hour && dt.Minute > _Target.Minute)
            {
                Console.WriteLine("Caculate TradeDay");
                Thread.Sleep(3600 * 1000);
            }
            Thread.Sleep(10 * 60 * 1000);
        }

    }
}
