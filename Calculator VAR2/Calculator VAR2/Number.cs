using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_VAR2
{
    class Number : INotifyPropertyChanged
    {

        public Number()
        {
            Nr_crt=0;
            sum=0;
            ecuation="";
        }
        private double _nrcrt;
        public double Nr_crt
        {
            get
            {
                return _nrcrt;
            }
            set
            {
                _nrcrt = value;
                NotifyPropertyChanged("Nr_crt");
            }
        }
        private double _sum;
        public double sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
                NotifyPropertyChanged("sum");
            }
        }
        private string _ecuation;
        public string ecuation
        {
            get
            {
                return _ecuation;
            }
            set
            {
                _ecuation = value;
                NotifyPropertyChanged("ecuation");
            }
        }

        public List<double> MemoryValues { get; set; } = new List<double>();


 
        public bool IsButtonEnabled
        {
            get
            {
                return MemoryValues != null && MemoryValues.Count > 0 && MemoryValues[0] != 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(propertyName));
        }
    }

}
