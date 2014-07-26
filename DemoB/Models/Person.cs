using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DemoB.Models
{
    public class Person : NotificationObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        public void Show(object parameter)
        {
            this.Name += "，你妈妈喊你回家吃饭了！";
        }
    }
}
