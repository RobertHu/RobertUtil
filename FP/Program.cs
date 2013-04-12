using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FP
 {

    class Identity<T>
    {
        public T Value { get; private set; }
        public Identity(T value)
        {
            this.Value = value;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var r = Bind(Unit(5), x => Bind(Unit(6), y => Unit(x + y)));
            Console.WriteLine(r.Value);
            Console.Read();

        }
        static Identity<T> Unit<T>(T value)
        {
            return new Identity<T>(value);
        }

        static Identity<U> Bind<T, U>(Identity<T> id, Func<T, Identity<U>> k)
        {
            return k(id.Value);
        }
    }
}
