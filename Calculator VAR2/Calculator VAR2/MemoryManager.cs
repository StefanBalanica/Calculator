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
        public Number DataContext = new Number();
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


        public void MemoryClick(string symbol, double value)
        {
            switch (symbol)
            {
                case "MC":
                    ClearMemory();
                    break;
                case "MR":
                    (DataContext as Number).Nr_crt= RecallLastValue();
                    break;
                case "M+":
                    memoryValues[0]+=value;
                    (DataContext as Number).Nr_crt= value;
                    break;
                case "M-":
                    memoryValues[0]-=value;
                    (DataContext as Number).Nr_crt= value;
                    break;
                case "MS":
                    StoreValue(value);
                    (DataContext as Number).Nr_crt= value;
                    break;
            }
            (DataContext as Number).MemoryValues = memoryValues;
        }
    }
}
