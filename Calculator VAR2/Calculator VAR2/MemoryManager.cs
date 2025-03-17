using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_VAR2
{
    internal class MemoryManager
    {
        private List<double> memoryValues = new List<double>();

        public void StoreValue(double value)
        {
            memoryValues.Insert(0, value);
        }

        public double RecallLastValue()
        {
            return memoryValues.Count > 0 ? memoryValues[0] : 0;
        }

        public void ClearMemory()
        {
            memoryValues.Clear();
        }
    }
}
