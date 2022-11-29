using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LowLevel.Services
{
    public class Counter
    {
        private int _teller = 0;

        public void Increment()
        {
            System.Console.WriteLine(++_teller);
        }
    }
}