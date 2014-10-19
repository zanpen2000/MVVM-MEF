using Commands;
using DemoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DemoB.ViewModels
{
    public class PersonViewModel : NotificationObject
    {
        public DelegateCommand PersonCommand { get; set; }

        public Person PersonModel { get; set; }


        public PersonViewModel()
        {
            this.PersonModel = new Person();
            
            this.PersonCommand = new DelegateCommand();
            //this.PersonCommand.CanExecuteFunc = this.PersonModel.CanExecuteFunction;
            this.PersonCommand.ExecuteAction = this.PersonModel.Show;
        }
    }
}
