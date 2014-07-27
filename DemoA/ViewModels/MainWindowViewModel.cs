using DemoA.Interfaces;
using DemoA.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoA.ViewModels
{

    class MainWindowViewModel : IAppHost
    {
        [Import(typeof(IWindowManager))]
        public IWindowManager win = null;

        private CompositionContainer _container;

        [Import(typeof(ICalc))]
        public ICalc Calc;

        [Export(typeof(MetaNumber))]
        public MetaNumber Metadata { get; set; }

        public DelegateCommand<string> OperateCommand { get; set; }

        public MainWindowViewModel()
        {
            this.Metadata = new MetaNumber();

            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);

            this.OperateCommand = new DelegateCommand<string>(this.Calc.DoCalc);

            ServiceLocator.AddService<IAppHost>(this);
        }

        public void ShowMessage(string msg)
        {
            win.ShowMessage(msg);
        }
    }
}
