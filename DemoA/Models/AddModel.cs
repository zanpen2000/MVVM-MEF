using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA.Models
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Method", "Add")]
    public class AddModel :  IOperation
    {

       
        public AddModel()
        {
            
        }

        [Import]
        public MetaNumber Metadata { get; set; }

        
        public void Operate()
        {
            this.Metadata.Result = this.Metadata.Input1 + this.Metadata.Input2;
        }


        internal void Save()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.ShowDialog();
        }
    }
}
