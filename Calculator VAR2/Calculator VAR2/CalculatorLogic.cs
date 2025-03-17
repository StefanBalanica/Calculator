using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator_VAR2
{
    internal class CalculatorLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public char lastOp = '0';
        private double storedValue = 0;
        char anteLastOp;
        public double Nr_crt = 0;
        double Nr_Here = 0;
        public Number DataContext = new Number();

        public void Calcul(char symbol, double value)
        {
            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '×':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '÷':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            lastOp=symbol;
            (DataContext as Number).ecuation=(DataContext as Number).sum.ToString() +symbol;
            Nr_crt=0;
        }

        public void Predefinit(string symbol, double value)
        {
            switch (symbol)
            {
                case "x²":
                    double aux = Nr_crt;
                    Nr_crt*=Nr_crt;
                    (DataContext as Number).ecuation= (DataContext as Number).ecuation +"sqrt(" +aux+")";
                    (DataContext as Number).Nr_crt=Nr_crt;
                    break;
                case "1/x":
                    double aux2 = Nr_crt;
                    Nr_crt= 1/Nr_crt;
                    (DataContext as Number).ecuation= (DataContext as Number).ecuation +"1/(" +aux2+")";
                    (DataContext as Number).Nr_crt=Nr_crt;
                    break;
                case "²√x":
                    double aux3 = Nr_crt;
                    Nr_crt=Math.Sqrt(Nr_crt);
                    (DataContext as Number).ecuation= (DataContext as Number).ecuation +"√" +aux3;
                    (DataContext as Number).Nr_crt=Nr_crt;
                    break;
                case "+/-":
                    Nr_crt*=-1;
                    (DataContext as Number).Nr_crt=Nr_crt;
                    break;
                case "%":
                    double aux4;
                    aux4 = ((DataContext as Number).sum * Nr_crt)/100;
                    Nr_crt = aux4;
                    (DataContext as Number).ecuation= (DataContext as Number).ecuation + Nr_crt.ToString();
                    (DataContext as Number).Nr_crt=Nr_crt;
                    break;

            }
        }
        public void Egal(ref bool isMoreThanOnce, ref bool isComaActive)
        {

            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '×':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '÷':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            if (!isMoreThanOnce)
            {
                (DataContext as Number).ecuation=(DataContext as Number).ecuation  + Nr_crt.ToString() +"=";
                isMoreThanOnce=true;
                anteLastOp=lastOp;
                Nr_Here = Nr_crt;
            }
            else
            {
                string[] parts = new string[0];
                switch (anteLastOp)
                {
                    case '0':
                        (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_Here;
                        break;
                    case '+':
                        (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('+');
                        break;
                    case '-':
                        (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('-');
                        break;
                    case '×':
                        (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('×');
                        break;
                    case '÷':
                        (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('÷');
                        break;
                }
                if (parts.Length > 1)
                {
                    (DataContext as Number).ecuation = (DataContext as Number).sum.ToString() +anteLastOp+ parts[1];
                    parts = new string[0];
                }
            }

            lastOp='0';
            (DataContext as Number).Nr_crt=(DataContext as Number).sum;
            Nr_crt=0;
            isComaActive=false;
        }
        public void Scrie_Nr(double value, bool isComaActive, ref double mpy)
        {
            if (isComaActive)
            {
                Nr_crt+=value *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+value;
            }
            (DataContext as Number).Nr_crt=Nr_crt;
        }
        public void Reset()
        {
            Nr_crt = 0;
            storedValue = 0;
            lastOp = '0';
            (DataContext as Number).Nr_crt=Nr_crt;
            (DataContext as Number).sum=storedValue;
        }
    }
}
