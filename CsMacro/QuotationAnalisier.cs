using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace CsMacro
{
    public class QuotationAnalisier
    {
        private QuotationAnalisier() 
        {
            Thread thread = new Thread(this.Handle);
            thread.IsBackground = true;
            thread.Start();
        }

        public static readonly QuotationAnalisier Default = new QuotationAnalisier();
        private List<int> _List = new List<int>();
        private object _Lock = new object();

        public void Add(int count)
        {
            lock (this._Lock)
            {
                this._List.Add(count);
            }
        }

        private void Handle()
        {
            while (true)
            {
                Thread.Sleep(5000);
                lock (_Lock)
                {
                    if (this._List.Count == 0)
                    {
                        continue;
                    }
                    int count = 0;
                    foreach (var item in this._List)
                    {
                        count += item;
                    }
                    this._List.Clear();
                    Console.WriteLine(string.Format("{0} receive quotations: {1}",DateTime.Now.ToString("HH:mm:ss"),count));
                }
            }
        }
    }
}
