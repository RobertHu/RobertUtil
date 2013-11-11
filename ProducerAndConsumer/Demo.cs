using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ProducerAndConsumer
{
    public class Demo
    {
        private const int MAX_SIZE=10;
        private List<int> _Products = new List<int>(MAX_SIZE);
        private Semaphore _SeFull = new Semaphore(0, MAX_SIZE);
        private Semaphore _SeEmpty = new Semaphore(MAX_SIZE, MAX_SIZE);
        private int  _InIndex = 0;
        private int _OutIndex = 0;

        public void Add(int item)
        {
            _SeEmpty.WaitOne();
            _InIndex = (_InIndex + 1) % MAX_SIZE;
            _Products[_InIndex] = item;
            _SeFull.Release();
        }

        private void Process()
        {
            while (true)
            {
                _SeFull.WaitOne();
                _OutIndex = (_OutIndex + 1) % MAX_SIZE;
                int item = _Products[_OutIndex];
                _SeEmpty.Release();
            }
        }
        
    }
}
