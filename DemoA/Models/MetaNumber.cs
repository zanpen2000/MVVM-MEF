using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA.Models
{

    public class MetaNumber : BindableBase
    {
        public MetaNumber()
        {

        }

        private double input1;

        public double Input1
        {
            get { return input1; }
            set
            {
                input1 = value;
                this.OnPropertyChanged("Input1");
            }
        }

        private double input2;

        public double Input2
        {
            get { return input2; }
            set
            {
                input2 = value;
                this.OnPropertyChanged("Input2");
            }
        }

        private double result;

        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                this.OnPropertyChanged("Result");
            }
        }
    }
}
