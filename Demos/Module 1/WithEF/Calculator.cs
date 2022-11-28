using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WithEF
{
    public class Calculator
    {
        public int Add(int a, int b) =>a+b;

        public int Subtract(int x, int y)
        {
            return x - y;
        }
    }
}