using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using DemoA.Interfaces;

namespace DemoA.Models
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Method", "Add")]
    public class AddModel :  IOperation
    {
        [Import]
        public MetaNumber Metadata { get; set; }

        
        public void Operate()
        {
            this.Metadata.Result = this.Metadata.Input1 + this.Metadata.Input2;
            IAppHost host = ServiceLocator.GetService<IAppHost>();
            host.ShowMessage("calc done!");
        }

        internal void Save()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.ShowDialog();
        }
    }
}
